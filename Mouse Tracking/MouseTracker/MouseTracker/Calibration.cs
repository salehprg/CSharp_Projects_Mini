using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace MouseTracker
{
    public partial class Calibration : Form
    {
        [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
        private static extern bool SetCursorPos(int x, int y);
        [DllImport("user32.dll", EntryPoint = "mouse_event")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        bool finished = false;
        static int result = 0;


        public Calibration()
        {
            InitializeComponent();
        }

        private void Calibration_Load(object sender, EventArgs e)
        {

            int mouseX = Screen.PrimaryScreen.Bounds.Width / 2;
            int mouseY = Screen.PrimaryScreen.Bounds.Height / 2;
            string[] mouseData = new string[] { mouseX.ToString() , mouseY.ToString() , "0" , "1", "1"};

            for (int j = 0; j < 20; j++)
            {
                dataGrid.Rows.Add(mouseData);
            }

            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value != 100)
            {
                progressBar1.Value += 100 / 20;
            }
            else if (finished)
            {
                finished = false;
                MessageBox.Show("کالیبره کردن با موفقیت انجام شد \n تاخیر سیستم : \n" + result.ToString() + " ms" , "کالیره", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                
            }
        }

        private void Calibration_Shown(object sender, EventArgs e)
        {
            result = 0;

            foreach (DataGridViewRow row in dataGrid.Rows)
            {
                DateTime before = DateTime.Now;

                int x = int.Parse(row.Cells[0].Value.ToString());
                int y = int.Parse(row.Cells[1].Value.ToString());
                int delay = int.Parse(row.Cells[2].Value.ToString());
                int action = int.Parse(row.Cells[4].Value.ToString());

                SetCursorPos(x, y);

                for (int i = 0; i < 20; i++)
                {
                    mouse_event(0x0002, x + i, y + i, 0, 0);
                    mouse_event(0x0004, x + i, y + i, 0, 0);
                }

                DateTime after = DateTime.Now;

                result += (after - before).Milliseconds;
            }

            result /= 20;

            Form1.systemicDelay = result;

            finished = true;
        }
    }
}
