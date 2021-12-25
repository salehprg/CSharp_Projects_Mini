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
    public partial class Settings : Form
    {

        public Settings()
        {
            InitializeComponent();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            switch(Form1.algorithm)
            {
                case 1:
                    rdbAlg1.Checked = true;
                    break;

                case 2:
                    rdbAlg2.Checked = true;
                    break;
            }

        }

        private void rdbAlg1_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbAlg1.Checked == true)
                Form1.algorithm = 1;
        }

        private void rdbAlg2_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbAlg2.Checked == true)
            {
                Calibration calibration = new Calibration();
                calibration.Show();

                Form1.algorithm = 2;
            }
        }
    }
}
