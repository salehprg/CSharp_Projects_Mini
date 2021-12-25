
namespace MouseTracker
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timerXY = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblY = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblX = new System.Windows.Forms.ToolStripStatusLabel();
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.colX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDelay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAction = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.action = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numLoopCount = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numLoopTime = new System.Windows.Forms.NumericUpDown();
            this.btnExecute = new System.Windows.Forms.Button();
            this.timerSchedule = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.فایلToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ذخیرهToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.بارگذاریToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.تنظیماتToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.راهنماToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblDelay = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblCounter = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblClkPSec = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblDelayLoop = new System.Windows.Forms.Label();
            this.lblDefDelay = new System.Windows.Forms.Label();
            this.numDelays = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.lblRealClk = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtMilScnd = new System.Windows.Forms.TextBox();
            this.btnSchedule = new System.Windows.Forms.Button();
            this.txtSecnd = new System.Windows.Forms.TextBox();
            this.txtMin = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtHour = new System.Windows.Forms.TextBox();
            this.lblTimer = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLoopCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLoopTime)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDelays)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // timerXY
            // 
            this.timerXY.Enabled = true;
            this.timerXY.Tick += new System.EventHandler(this.timerXY_Tick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus,
            this.lblY,
            this.lblX});
            this.statusStrip1.Location = new System.Drawing.Point(0, 442);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(470, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(38, 17);
            this.lblStatus.Text = "آماده !";
            // 
            // lblY
            // 
            this.lblY.Name = "lblY";
            this.lblY.Size = new System.Drawing.Size(118, 17);
            this.lblY.Text = "toolStripStatusLabel1";
            // 
            // lblX
            // 
            this.lblX.Name = "lblX";
            this.lblX.Size = new System.Drawing.Size(118, 17);
            this.lblX.Text = "toolStripStatusLabel1";
            // 
            // dataGrid
            // 
            this.dataGrid.AllowDrop = true;
            this.dataGrid.AllowUserToAddRows = false;
            this.dataGrid.AllowUserToOrderColumns = true;
            this.dataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colX,
            this.colY,
            this.colDelay,
            this.colAction,
            this.action});
            this.dataGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.dataGrid.Location = new System.Drawing.Point(8, 28);
            this.dataGrid.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dataGrid.Size = new System.Drawing.Size(450, 172);
            this.dataGrid.TabIndex = 9;
            this.dataGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid_CellValueChanged);
            this.dataGrid.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGrid_RowsAdded);
            this.dataGrid.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dataGrid_RowsRemoved);
            // 
            // colX
            // 
            this.colX.HeaderText = "X";
            this.colX.Name = "colX";
            // 
            // colY
            // 
            this.colY.HeaderText = "Y";
            this.colY.Name = "colY";
            // 
            // colDelay
            // 
            this.colDelay.HeaderText = "تاخیر";
            this.colDelay.Name = "colDelay";
            // 
            // colAction
            // 
            this.colAction.HeaderText = "عملکرد";
            this.colAction.Name = "colAction";
            this.colAction.ReadOnly = true;
            // 
            // action
            // 
            this.action.HeaderText = "colAction";
            this.action.Name = "action";
            this.action.ReadOnly = true;
            this.action.Visible = false;
            // 
            // numLoopCount
            // 
            this.numLoopCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numLoopCount.Location = new System.Drawing.Point(318, 360);
            this.numLoopCount.Name = "numLoopCount";
            this.numLoopCount.Size = new System.Drawing.Size(65, 27);
            this.numLoopCount.TabIndex = 12;
            this.numLoopCount.ValueChanged += new System.EventHandler(this.numLoopCount_ValueChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(384, 362);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 20);
            this.label1.TabIndex = 13;
            this.label1.Text = "تعداد تکرار : ";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(347, 325);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 20);
            this.label2.TabIndex = 14;
            this.label2.Text = "زمان هر حلقه (ms) :";
            // 
            // numLoopTime
            // 
            this.numLoopTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numLoopTime.Location = new System.Drawing.Point(271, 323);
            this.numLoopTime.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.numLoopTime.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numLoopTime.Name = "numLoopTime";
            this.numLoopTime.Size = new System.Drawing.Size(70, 27);
            this.numLoopTime.TabIndex = 15;
            this.numLoopTime.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.numLoopTime.ValueChanged += new System.EventHandler(this.numDelay_ValueChanged);
            // 
            // btnExecute
            // 
            this.btnExecute.Location = new System.Drawing.Point(109, 5);
            this.btnExecute.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(128, 33);
            this.btnExecute.TabIndex = 17;
            this.btnExecute.Text = "اجرا لحظه (F3)";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.button3_Click);
            // 
            // timerSchedule
            // 
            this.timerSchedule.Enabled = true;
            this.timerSchedule.Interval = 20;
            this.timerSchedule.Tick += new System.EventHandler(this.timerSchedule_Tick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.فایلToolStripMenuItem,
            this.تنظیماتToolStripMenuItem,
            this.راهنماToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(470, 24);
            this.menuStrip1.TabIndex = 22;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // فایلToolStripMenuItem
            // 
            this.فایلToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ذخیرهToolStripMenuItem,
            this.بارگذاریToolStripMenuItem});
            this.فایلToolStripMenuItem.Name = "فایلToolStripMenuItem";
            this.فایلToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.فایلToolStripMenuItem.Text = "فایل";
            // 
            // ذخیرهToolStripMenuItem
            // 
            this.ذخیرهToolStripMenuItem.Name = "ذخیرهToolStripMenuItem";
            this.ذخیرهToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.ذخیرهToolStripMenuItem.Text = "ذخیره";
            this.ذخیرهToolStripMenuItem.Click += new System.EventHandler(this.ذخیرهToolStripMenuItem_Click);
            // 
            // بارگذاریToolStripMenuItem
            // 
            this.بارگذاریToolStripMenuItem.Name = "بارگذاریToolStripMenuItem";
            this.بارگذاریToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.بارگذاریToolStripMenuItem.Text = "بارگذاری";
            this.بارگذاریToolStripMenuItem.Click += new System.EventHandler(this.بارگذاریToolStripMenuItem_Click);
            // 
            // تنظیماتToolStripMenuItem
            // 
            this.تنظیماتToolStripMenuItem.Name = "تنظیماتToolStripMenuItem";
            this.تنظیماتToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.تنظیماتToolStripMenuItem.Text = "تنظیمات";
            this.تنظیماتToolStripMenuItem.Click += new System.EventHandler(this.تنظیماتToolStripMenuItem_Click);
            // 
            // راهنماToolStripMenuItem
            // 
            this.راهنماToolStripMenuItem.Name = "راهنماToolStripMenuItem";
            this.راهنماToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.راهنماToolStripMenuItem.Text = "راهنما";
            this.راهنماToolStripMenuItem.Click += new System.EventHandler(this.راهنماToolStripMenuItem_Click);
            // 
            // lblDelay
            // 
            this.lblDelay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDelay.AutoSize = true;
            this.lblDelay.Location = new System.Drawing.Point(337, 205);
            this.lblDelay.Name = "lblDelay";
            this.lblDelay.Size = new System.Drawing.Size(17, 20);
            this.lblDelay.TabIndex = 23;
            this.lblDelay.Text = "0";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(384, 205);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 20);
            this.label8.TabIndex = 24;
            this.label8.Text = "جمع تاخیر ها :";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(194, 206);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(91, 20);
            this.label7.TabIndex = 26;
            this.label7.Text = "تعداد دستورات :";
            // 
            // lblCounter
            // 
            this.lblCounter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCounter.AutoSize = true;
            this.lblCounter.Location = new System.Drawing.Point(155, 206);
            this.lblCounter.Name = "lblCounter";
            this.lblCounter.Size = new System.Drawing.Size(17, 20);
            this.lblCounter.TabIndex = 25;
            this.lblCounter.Text = "0";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(57, 205);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(73, 20);
            this.label10.TabIndex = 28;
            this.label10.Text = "کلیک/ثانیه : ";
            // 
            // lblClkPSec
            // 
            this.lblClkPSec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblClkPSec.AutoSize = true;
            this.lblClkPSec.Location = new System.Drawing.Point(26, 205);
            this.lblClkPSec.Name = "lblClkPSec";
            this.lblClkPSec.Size = new System.Drawing.Size(25, 20);
            this.lblClkPSec.TabIndex = 27;
            this.lblClkPSec.Text = "50";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(373, 229);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 20);
            this.label6.TabIndex = 30;
            this.label6.Text = "تاخیر پایان لوپ :";
            // 
            // lblDelayLoop
            // 
            this.lblDelayLoop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDelayLoop.AutoSize = true;
            this.lblDelayLoop.Location = new System.Drawing.Point(328, 229);
            this.lblDelayLoop.Name = "lblDelayLoop";
            this.lblDelayLoop.Size = new System.Drawing.Size(17, 20);
            this.lblDelayLoop.TabIndex = 29;
            this.lblDelayLoop.Text = "0";
            // 
            // lblDefDelay
            // 
            this.lblDefDelay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDefDelay.AutoSize = true;
            this.lblDefDelay.Location = new System.Drawing.Point(370, 399);
            this.lblDefDelay.Name = "lblDefDelay";
            this.lblDefDelay.Size = new System.Drawing.Size(88, 20);
            this.lblDefDelay.TabIndex = 32;
            this.lblDefDelay.Text = "تکرار زمانی (s):";
            // 
            // numDelays
            // 
            this.numDelays.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numDelays.Location = new System.Drawing.Point(295, 397);
            this.numDelays.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numDelays.Name = "numDelays";
            this.numDelays.Size = new System.Drawing.Size(69, 27);
            this.numDelays.TabIndex = 31;
            this.numDelays.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numDelays.ValueChanged += new System.EventHandler(this.numLoopTime_ValueChanged);
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(65, 229);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 20);
            this.label9.TabIndex = 35;
            this.label9.Text = "انجام شده :";
            // 
            // lblRealClk
            // 
            this.lblRealClk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRealClk.AutoSize = true;
            this.lblRealClk.Location = new System.Drawing.Point(26, 229);
            this.lblRealClk.Name = "lblRealClk";
            this.lblRealClk.Size = new System.Drawing.Size(17, 20);
            this.lblRealClk.TabIndex = 38;
            this.lblRealClk.Text = "0";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnExecute);
            this.panel1.Location = new System.Drawing.Point(215, 263);
            this.panel1.Name = "panel1";
            this.panel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.panel1.Size = new System.Drawing.Size(243, 54);
            this.panel1.TabIndex = 41;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.txtMilScnd);
            this.panel2.Controls.Add(this.btnSchedule);
            this.panel2.Controls.Add(this.txtSecnd);
            this.panel2.Controls.Add(this.txtMin);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.txtHour);
            this.panel2.Controls.Add(this.lblTimer);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Location = new System.Drawing.Point(8, 263);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(201, 168);
            this.panel2.TabIndex = 42;
            // 
            // txtMilScnd
            // 
            this.txtMilScnd.Font = new System.Drawing.Font("B Yekan", 13.5F);
            this.txtMilScnd.Location = new System.Drawing.Point(126, 119);
            this.txtMilScnd.MaxLength = 3;
            this.txtMilScnd.Name = "txtMilScnd";
            this.txtMilScnd.Size = new System.Drawing.Size(39, 35);
            this.txtMilScnd.TabIndex = 24;
            this.txtMilScnd.Text = "0";
            // 
            // btnSchedule
            // 
            this.btnSchedule.Location = new System.Drawing.Point(23, 5);
            this.btnSchedule.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSchedule.Name = "btnSchedule";
            this.btnSchedule.Size = new System.Drawing.Size(149, 33);
            this.btnSchedule.TabIndex = 10;
            this.btnSchedule.Text = "اجرا در ساعت (F4)";
            this.btnSchedule.UseVisualStyleBackColor = true;
            this.btnSchedule.Click += new System.EventHandler(this.btnSchedule_Click);
            // 
            // txtSecnd
            // 
            this.txtSecnd.Font = new System.Drawing.Font("B Yekan", 13.5F);
            this.txtSecnd.Location = new System.Drawing.Point(86, 119);
            this.txtSecnd.MaxLength = 2;
            this.txtSecnd.Name = "txtSecnd";
            this.txtSecnd.Size = new System.Drawing.Size(33, 35);
            this.txtSecnd.TabIndex = 23;
            this.txtSecnd.TextChanged += new System.EventHandler(this.txtSecnd_TextChanged);
            // 
            // txtMin
            // 
            this.txtMin.Font = new System.Drawing.Font("B Yekan", 13.5F);
            this.txtMin.Location = new System.Drawing.Point(45, 119);
            this.txtMin.MaxLength = 2;
            this.txtMin.Name = "txtMin";
            this.txtMin.Size = new System.Drawing.Size(33, 35);
            this.txtMin.TabIndex = 22;
            this.txtMin.TextChanged += new System.EventHandler(this.txtMin_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("B Yekan", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(6, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(152, 18);
            this.label3.TabIndex = 17;
            this.label3.Text = "دقت تایمر 20 میلی ثانیه میباشد";
            // 
            // txtHour
            // 
            this.txtHour.Font = new System.Drawing.Font("B Yekan", 13.5F);
            this.txtHour.Location = new System.Drawing.Point(6, 119);
            this.txtHour.MaxLength = 2;
            this.txtHour.Name = "txtHour";
            this.txtHour.Size = new System.Drawing.Size(33, 35);
            this.txtHour.TabIndex = 21;
            this.txtHour.TextChanged += new System.EventHandler(this.txtHour_TextChanged);
            // 
            // lblTimer
            // 
            this.lblTimer.AutoSize = true;
            this.lblTimer.Font = new System.Drawing.Font("B Yekan", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.lblTimer.ForeColor = System.Drawing.Color.Red;
            this.lblTimer.Location = new System.Drawing.Point(3, 52);
            this.lblTimer.Name = "lblTimer";
            this.lblTimer.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblTimer.Size = new System.Drawing.Size(96, 24);
            this.lblTimer.TabIndex = 19;
            this.lblTimer.Text = "hh:mm:ss:ff";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("B Yekan", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label11.Location = new System.Drawing.Point(35, 124);
            this.label11.Name = "label11";
            this.label11.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label11.Size = new System.Drawing.Size(95, 24);
            this.label11.TabIndex = 25;
            this.label11.Text = ":       :       .";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(470, 464);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblRealClk);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lblDefDelay);
            this.Controls.Add(this.numDelays);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblDelayLoop);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.lblClkPSec);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblCounter);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lblDelay);
            this.Controls.Add(this.numLoopTime);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numLoopCount);
            this.Controls.Add(this.dataGrid);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximumSize = new System.Drawing.Size(486, 532);
            this.Name = "Form1";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SAD";
            this.Activated += new System.EventHandler(this.Form1_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLoopCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLoopTime)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDelays)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer timerXY;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.DataGridView dataGrid;
        private System.Windows.Forms.NumericUpDown numLoopCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numLoopTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colX;
        private System.Windows.Forms.DataGridViewTextBoxColumn colY;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDelay;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAction;
        private System.Windows.Forms.DataGridViewTextBoxColumn action;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.Timer timerSchedule;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem راهنماToolStripMenuItem;
        private System.Windows.Forms.Label lblDelay;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblCounter;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblClkPSec;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblDelayLoop;
        private System.Windows.Forms.ToolStripMenuItem فایلToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ذخیرهToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem بارگذاریToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel lblY;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolStripStatusLabel lblX;
        private System.Windows.Forms.Label lblDefDelay;
        private System.Windows.Forms.NumericUpDown numDelays;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblRealClk;
        private System.Windows.Forms.ToolStripMenuItem تنظیماتToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtMilScnd;
        private System.Windows.Forms.Button btnSchedule;
        private System.Windows.Forms.TextBox txtSecnd;
        private System.Windows.Forms.TextBox txtMin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtHour;
        private System.Windows.Forms.Label lblTimer;
        private System.Windows.Forms.Label label11;
    }
}

