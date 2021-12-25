
namespace MouseTracker
{
    partial class Calibration
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
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.colX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDelay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAction = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.action = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("B Yekan", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label1.Location = new System.Drawing.Point(42, 31);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(249, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "درحال کالیبره کردن لطفا منتظر بمانید ....";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(42, 83);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(245, 23);
            this.progressBar1.TabIndex = 1;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
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
            this.dataGrid.Location = new System.Drawing.Point(162, 60);
            this.dataGrid.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dataGrid.Size = new System.Drawing.Size(153, 62);
            this.dataGrid.TabIndex = 10;
            this.dataGrid.Visible = false;
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
            // Calibration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(328, 137);
            this.Controls.Add(this.dataGrid);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Calibration";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "کالیبره کردن";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Calibration_Load);
            this.Shown += new System.EventHandler(this.Calibration_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.DataGridView dataGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn colX;
        private System.Windows.Forms.DataGridViewTextBoxColumn colY;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDelay;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAction;
        private System.Windows.Forms.DataGridViewTextBoxColumn action;
    }
}