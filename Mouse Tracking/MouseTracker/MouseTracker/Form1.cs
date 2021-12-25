using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tarczynski.NtpDateTime;
using Yort.
    Ntp;

namespace MouseTracker
{
    delegate void SetTextCallback(string text);
    delegate void SetStateCallback(FormWindowState state);
    public partial class Form1 : Form
    {
        private const int MOUSEEVENTF_MOVE = 0x0001; /* mouse move */
        private const int MOUSEEVENTF_LEFTDOWN = 0x0002; /* left button down */
        private const int MOUSEEVENTF_LEFTUP = 0x0004; /* left button up */
        private const int MOUSEEVENTF_RIGHTDOWN = 0x0008; /* right button down */
        enum KeyModifier
        {
            None = 0,
            Alt = 1,
            Control = 2,
            Shift = 4,
            WinKey = 8
        }
        const int singleClk = 0;
        const int doubleClk = 1;
        const int cancelation = 2;
        const int execute = 3;
        const int executeOnTime = 4;


        [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
        private static extern bool SetCursorPos(int x, int y);
        [DllImport("user32.dll", EntryPoint = "mouse_event")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons,int dwExtraInfo);
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);
        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);


        public static int algorithm = 1;
        public static int systemicDelay = 7;

        static DateTime schedule;
        static DateTime loopEndTimer;
        static double offset = 0;

        static Image image;
        static bool saved = false;
        static bool executing = false;
        static bool timeRunning = false;
        Clock clock = new Clock();
        Thread runningThread;

        int totalDelay = 0;
        int realDelay = 0;
        int realClk = 0;
        int loopDelay
        {
            get
            {
                int result = 0;

                switch (algorithm)
                {
                    case 1:
                        result = int.Parse(numLoopTime.Value.ToString()) - totalDelay;
                        break;

                    case 2:
                        result = realLoopDelay;
                        break;
                }

                 return (result < 0 ? 0 : result);
            }
        }

        int realLoopDelay
        {
            get
            {
                int result = int.Parse(numLoopTime.Value.ToString()) - realDelay;

                return (result < 0 ? 0 : result);
            }
        }


    protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == 0x0312)
            {
                Keys key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);                  // The key of the hotkey that was pressed.
                KeyModifier modifier = (KeyModifier)((int)m.LParam & 0xFFFF);       // The modifier of the hotkey that was pressed.
                int id = m.WParam.ToInt32();                                        // The id of the hotkey that was pressed.

                switch (id)
                {
                    case singleClk:
                        addData(false);
                        break;

                    case doubleClk:
                        addData(true);
                        break;

                    case cancelation:
                        if (executing)
                        {
                            runningThread.Abort();
                            timeRunning = false;
                            executing = false;
                            this.WindowState = FormWindowState.Normal;
                            //MessageBox.Show("انجام دستورات متوقف شد", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        break;

                    case execute:
                        if (!executing)
                        {
                            StartExecute();
                        }

                        break;

                    case executeOnTime:
                        StartSchedule();
                        break;
                }
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RegisterHotKey(this.Handle, singleClk, (int)KeyModifier.None, Keys.F1.GetHashCode());
            RegisterHotKey(this.Handle, doubleClk, (int)KeyModifier.None, Keys.F2.GetHashCode());
            RegisterHotKey(this.Handle, cancelation, (int)KeyModifier.None, Keys.Escape.GetHashCode());
            RegisterHotKey(this.Handle, execute, (int)KeyModifier.None, Keys.F3.GetHashCode());
            RegisterHotKey(this.Handle, executeOnTime, (int)KeyModifier.None, Keys.F4.GetHashCode());

            double clockOffset = clock.GetOffset();
            if (clockOffset != -1)
            {
                offset = clockOffset;
            }
            else
            {
                offset = 0;
                MessageBox.Show("برقراری ارتباط با سرور موجود نمیباشد. ممکن است زمان نشان داده شده با زمان جهانی تفاوت داشته باشد." , "WARNING" , MessageBoxButtons.OK , MessageBoxIcon.Warning);
            }
        }


        private void timerXY_Tick(object sender, EventArgs e)
        {
            lblX.Text = "X : " + Cursor.Position.X.ToString();
            lblY.Text = "Y : " + Cursor.Position.Y.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            StartExecute();
            
        }

        private void timerSchedule_Tick(object sender, EventArgs e)
        {

            lblTimer.Text = clock.getTimeNow().ToString("H:mm:ss.fff");
            
            if(schedule != DateTime.MinValue)
            {
                DateTime now = clock.getTimeNow();
                if (now >= schedule)
                {
                    StartExecute();

                    schedule = DateTime.MinValue;
                    //btnImage.Enabled = true;
                }
            }
        }

        void StartSchedule()
        {
            try
            {
                //btnImage.Enabled = false;
 
                int year = clock.getTimeNow().Year;
                int month = clock.getTimeNow().Month;
                int day = clock.getTimeNow().Day;
                int hr = int.Parse(txtHour.Text);
                int mn = int.Parse(txtMin.Text);
                int ss = int.Parse(txtSecnd.Text);
                int ms = int.Parse(txtMilScnd.Text);

                schedule = new DateTime(year, month, day, hr, mn, ss, ms);
                lblStatus.Text = "اجرا دستورات در زمان " + schedule.ToString("H:mm:ss.fff");
                //MessageBox.Show("دستورات در زمان " + schedule.ToString("H:mm:ss.fff") + " اجرا میشوند " , "INFO" , MessageBoxButtons.OK , MessageBoxIcon.Information);
            }
            catch
            {
                //btnImage.Enabled = true;
            }
        }

        private void btnSchedule_Click(object sender, EventArgs e)
        {
            StartSchedule();

        }

        private void dataGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            calculateInfo();
        }

        private void dataGrid_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            calculateInfo();
        }


 #region Functions

        void addData(bool doubleClick)
        {
            int delay = 5;
            string action = (doubleClick ? "دبل کلیک" : "کلیک");

            string[] mouseData = new string[] { Cursor.Position.X.ToString(), Cursor.Position.Y.ToString(), delay.ToString(), action, (doubleClick ? "2" : "1") };
            if (dataGrid.SelectedRows.Count > 0)
            {
                for (int i = 0; i < dataGrid.ColumnCount; i++)
                {
                    if(i != 2)//Ignore Delay cell
                        dataGrid.Rows[dataGrid.SelectedRows[0].Index].Cells[i].Value = mouseData[i];
                }
            }
            else
            {
                dataGrid.Rows.Add(mouseData);
            }
        }
            
        void StartExecute()
        {
            if (!executing)
            {
                executing = true;
                lblStatus.Text = "درحال انجام دستورات ...";
                this.WindowState = FormWindowState.Minimized;
                realClk = 0;

                if (numLoopCount.Value > 0)
                {
                    runningThread = new Thread(ExecuteData);
                    runningThread.Start();
                }
                else if (numDelays.Value > 0)
                {
                    
                    runningThread = new Thread(ExecuteTimeData);
                    runningThread.Start();
                           
                }  
            }
        }
        private void SetTextRealClk(string text)
        {
            lblRealClk.Text = text;
        }
        private void SetTextStatus(string text)
        {
            lblStatus.Text = text;
        }
        private void SetWindowsState(FormWindowState state)
        {
            this.WindowState = state;
        }


        void UpdateInfo()
        {
            loopEndTimer = DateTime.MinValue;
            if (lblRealClk.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetTextRealClk);
                Invoke(d, new object[] { realClk.ToString()});

                d = new SetTextCallback(SetTextStatus);
                Invoke(d, new object[] { "انجام دستورات پایان یافت" });

                SetStateCallback s = new SetStateCallback(SetWindowsState);
                Invoke(s, new object[] { FormWindowState.Normal });

            }

            executing = false;
            //MessageBox.Show("انجام دستورات پایان یافت", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        void ExecuteData()
        {
            int loopCount = int.Parse(numLoopCount.Value.ToString());
            
            for (int i = 1; i <= (loopCount == 0 ? 1 : loopCount); i++)
            {
                foreach (DataGridViewRow row in dataGrid.Rows)
                {
                    DoActions(row);
                }
                Thread.Sleep(loopDelay);
            }
            UpdateInfo();
        }
        void ExecuteTimeData()
        {
            int timerSecond = int.Parse(numDelays.Value.ToString());

            DateTime endTime = clock.getTimeNow().AddSeconds(timerSecond);
            loopEndTimer = endTime;

            while (clock.getTimeNow() <= endTime)
            {
                if (!timeRunning)
                {
                    timeRunning = true;

                    foreach (DataGridViewRow row in dataGrid.Rows)
                    {
                        if (clock.getTimeNow() <= loopEndTimer)
                        {
                            DoActions(row);
                        }
                    }

                    Thread.Sleep(loopDelay);

                    timeRunning = false;
                }
            }

            UpdateInfo();
        }

        void DoActions(DataGridViewRow row)
        {

            int x = int.Parse(row.Cells[0].Value.ToString());
            int y = int.Parse(row.Cells[1].Value.ToString());
            int delay = int.Parse(row.Cells[2].Value.ToString());
            int action = int.Parse(row.Cells[4].Value.ToString());
            int loop = action;

            SetCursorPos(x, y);
            for (int j = 0; j < loop; j++)
            {
                mouse_event(MOUSEEVENTF_LEFTDOWN, x, y, 0, 0);
                mouse_event(MOUSEEVENTF_LEFTUP, x, y, 0, 0);
            }

            realClk++;

            Thread.Sleep(delay);

        }


        void calculateInfo()
        {
            saved = false;
            int execCount = dataGrid.Rows.Count;

            lblCounter.Text = execCount.ToString();
            int delays = 0;

            foreach (DataGridViewRow row in dataGrid.Rows)
            {
                delays += int.Parse(row.Cells[2].Value.ToString());
            }

            totalDelay = delays;
            realDelay = delays + execCount * systemicDelay;

            if(totalDelay > numLoopTime.Value)
            {
                numLoopTime.Value = totalDelay;
            }

            int clcPSec = 0;
            int everyLoopDelay = int.Parse(numLoopTime.Value.ToString());
            int totalRunTime = int.Parse(numDelays.Value.ToString());

            int loopCount = int.Parse(numLoopCount.Value.ToString());

            switch (algorithm)
            {
                case 2:
                    try
                    {
                        if (1000 % everyLoopDelay > realDelay)
                        {
                            clcPSec = int.Parse(Math.Ceiling(1000.0 / everyLoopDelay).ToString()) * execCount;
                        }
                        else
                        {
                            clcPSec = (1000 / everyLoopDelay) * execCount;
                        }
                    }
                    catch { }
                    break;

                case 1:
                    try
                    {
                        clcPSec = (1000 / everyLoopDelay) * execCount;
                    }
                    catch { }
                    break;
            }



            lblClkPSec.Text = clcPSec.ToString();

            lblDelayLoop.Text = (int.Parse(numLoopTime.Value.ToString()) - totalDelay).ToString();
            lblDelay.Text = delays.ToString();
            //lblRealDelay.Text = realDelay.ToString();
            //lblRealLoopDelay.Text = realLoopDelay.ToString();
        }

        void Save()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "AutoClickerData (*.cac;)|*.cac";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string content = "";
                foreach (DataGridViewRow row in dataGrid.Rows)
                {
                    for (int i = 0; i < dataGrid.ColumnCount; i++)
                    {
                        content += row.Cells[i].Value + ",";
                    }

                    if(row.Index + 1 != dataGrid.RowCount)
                        content += "|";
                }
                content += "\n";
                content += numLoopCount.Value + "\n";
                content += numLoopTime.Value + "\n";
                content += txtHour.Text + "\n";
                content += txtMin.Text + "\n";
                content += txtSecnd.Text + "\n";
                content += txtMilScnd.Text + "\n";
                content += algorithm.ToString() + "\n";

                File.WriteAllText(saveFileDialog.FileName, content);

                saved = true;
            }

        }
        void LoadData()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "AutoClickerData (*.cac;)|*.cac";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                dataGrid.Rows.Clear();

                string content = File.ReadAllText(openFileDialog.FileName);
                string[] allData = content.Split('\n');

                string[] gridData = allData[0].Split('|');

                foreach (string data in gridData)
                {
                    string[] cellInfo = data.Split(',');

                    string[] mouseData = new string[] { cellInfo[0], cellInfo[1], cellInfo[2], cellInfo[3], cellInfo[4] };
                    dataGrid.Rows.Add(mouseData);
                }
                numLoopCount.Value = decimal.Parse(allData[1]);
                numLoopTime.Value = decimal.Parse(allData[2]);
                txtHour.Text = allData[3];
                txtMin.Text = allData[4];
                txtSecnd.Text = allData[5];
                txtMilScnd.Text = allData[6];
                algorithm = int.Parse(allData[7]);

                calculateInfo();
            }

            
        }


#endregion

        private void btnImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();

            open.Filter = "Image Files(*.jpg; *.jpeg; *.bmp)|*.jpg; *.jpeg; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                //pictureBox1.Image = new Bitmap(open.FileName);
            }

            lblStatus.Text = "اجرا با ظاهر شدن تصویر";
        }

        private void راهنماToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help help = new Help();
            help.Show();
        }

        private void dataGrid_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            calculateInfo();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!saved && MessageBox.Show("آیا میخواهید تنظیمات را ذخیره کنید ؟", "ذخیره", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                Save();
            }
            else
            {
                saved = true;
                Application.Exit();
            }
        }

        private void numDelay_ValueChanged(object sender, EventArgs e)
        {
            calculateInfo();
        }

        private void ذخیرهToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void بارگذاریToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void numLoopCount_ValueChanged(object sender, EventArgs e)
        {
            numDelays.Value = 0;
        }

        private void numLoopTime_ValueChanged(object sender, EventArgs e)
        {
            numLoopCount.Value = 0;
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            calculateInfo();
        }

        private void تنظیماتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.Show();
        }

        private void txtHour_TextChanged(object sender, EventArgs e)
        {
            if (txtHour.TextLength == 2)
                txtMin.Focus();
        }

        private void txtMin_TextChanged(object sender, EventArgs e)
        {
            if (txtMin.TextLength == 2)
                txtSecnd.Focus();
        }

        private void txtSecnd_TextChanged(object sender, EventArgs e)
        {
            if (txtSecnd.TextLength == 2)
                txtMilScnd.Focus();
        }
    }
}
