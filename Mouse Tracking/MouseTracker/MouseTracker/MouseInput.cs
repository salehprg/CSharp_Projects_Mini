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
    public partial class MouseInput : Form
    {
        public MouseInput()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int x = Cursor.Position.X;
            int y = Cursor.Position.Y;

            txtX.Text = x.ToString();
            txtY.Text = y.ToString();

            this.Location = new Point(x + 20, y + 20);
        }

    }
}
