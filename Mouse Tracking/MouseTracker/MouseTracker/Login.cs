using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using Yort.Ntp;

namespace MouseTracker
{
    public partial class Login : Form
    {
        bool LogedIn = false;

        public Login()
        {
            InitializeComponent();
        }

        //DateTime trialTime = new DateTime(2021, 2, 4, 23, 59, 59);
        DateTime currentTime;
        private void button1_Click(object sender, EventArgs e)
        {
            //if (currentTime <= trialTime)
            //{
                if (txtUsername.Text == "SAD" && txtPassword.Text == "987")
                {
                    string hashData = PcInfo.GetPcHashInfo();

                    string passwords = File.ReadAllText(Directory.GetCurrentDirectory() + "/credential.crd");
                    File.WriteAllText(Directory.GetCurrentDirectory() + "/credential.crd", passwords + "\n" + hashData);

                    Form1 main = new Form1();
                    main.Show();
                    this.Hide();
                }
                else
                {
                    lblMsg.Text = "نام کاربری یا رمز اشتباه است";
                }
            //}
            //else
            //{
            //    lblMsg.Text = "زمان تست برنامه به اتمام رسیده است";
            //}
        }

        private void Login_Load(object sender, EventArgs e)
        {
            Clock clock = new Clock();
            double offset = clock.GetOffset();

            if(offset != -1)
            { 
                currentTime = DateTime.Now.AddMilliseconds(offset);
            }
            else
            {
                MessageBox.Show("لطفا ارتباط خود را با اینترنت بررسی نمایید.");
                Application.Exit();
            }

            try
            {
                if(!File.Exists(Directory.GetCurrentDirectory() + "/credential.crd"))
                {
                    File.Create(Directory.GetCurrentDirectory() + "/credential.crd").Close();
                }

                string hashData = PcInfo.GetPcHashInfo();
                string[] passwords = File.ReadAllText(Directory.GetCurrentDirectory() + "/credential.crd").Split('\n');

                foreach (string password in passwords)
                {
                    //if (password == hashData && currentTime <= trialTime)
                    //{
                    //    LogedIn = true;
                    //}
                    if (password == hashData)
                    {
                        LogedIn = true;
                    }
                }

            }
            catch(Exception ex)
            {

            }
        }

        private void Login_Shown(object sender, EventArgs e)
        {
            if(LogedIn)
            {
                Form1 form1 = new Form1();
                form1.Show();
                this.Hide();
            }
        }
    }
}
