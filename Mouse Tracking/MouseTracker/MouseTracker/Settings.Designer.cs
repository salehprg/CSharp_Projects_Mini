
namespace MouseTracker
{
    partial class Settings
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
            this.rdbAlg1 = new System.Windows.Forms.RadioButton();
            this.rdbAlg2 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rdbAlg1
            // 
            this.rdbAlg1.AutoSize = true;
            this.rdbAlg1.Location = new System.Drawing.Point(234, 26);
            this.rdbAlg1.Name = "rdbAlg1";
            this.rdbAlg1.Size = new System.Drawing.Size(92, 24);
            this.rdbAlg1.TabIndex = 0;
            this.rdbAlg1.TabStop = true;
            this.rdbAlg1.Text = "الگوریتم اول";
            this.rdbAlg1.UseVisualStyleBackColor = true;
            this.rdbAlg1.CheckedChanged += new System.EventHandler(this.rdbAlg1_CheckedChanged);
            // 
            // rdbAlg2
            // 
            this.rdbAlg2.AutoSize = true;
            this.rdbAlg2.Location = new System.Drawing.Point(176, 107);
            this.rdbAlg2.Name = "rdbAlg2";
            this.rdbAlg2.Size = new System.Drawing.Size(150, 24);
            this.rdbAlg2.TabIndex = 1;
            this.rdbAlg2.TabStop = true;
            this.rdbAlg2.Text = "الگوریتم دوم (پیشنهادی)";
            this.rdbAlg2.UseVisualStyleBackColor = true;
            this.rdbAlg2.CheckedChanged += new System.EventHandler(this.rdbAlg2_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.rdbAlg1);
            this.groupBox1.Controls.Add(this.rdbAlg2);
            this.groupBox1.Location = new System.Drawing.Point(46, 33);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(341, 204);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "محاسبه تعداد کلیک براساس ";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("B Yekan", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(6, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(306, 40);
            this.label1.TabIndex = 2;
            this.label1.Text = "در این الگوریتم محاسبه کلیک براساس تاخیر وارده شده و مبنای محاسبات اعداد ورودی از" +
    " طرف کاربر میباشد ";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("B Yekan", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(6, 134);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(306, 67);
            this.label2.TabIndex = 3;
            this.label2.Text = "در الگوریتم دوم محاسبه کلیک براساس تاخیر وارده شده به علاوه تاخیر سیستمی میباشد و" +
    " مبنای محاسبات اعداد ورودی و تاخیر سیستمی دستورات میباشد ";
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(441, 297);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Settings";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.Settings_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton rdbAlg1;
        private System.Windows.Forms.RadioButton rdbAlg2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}