using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;

namespace MouseTracker
{
    class PcInfo
    {
        public static string GetMotherBoardSerial()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher
                    ("SELECT SerialNumber FROM Win32_BaseBoard");

            ManagementObjectCollection mObject = searcher.Get();

            foreach (ManagementObject obj in mObject)
            {
                return obj.Properties["SerialNumber"].Value.ToString();
            }

            return "";
        }

        public static string GetCPUSerial()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher
                ("SELECT ProcessorId FROM Win32_Processor");
            ManagementObjectCollection mObject = searcher.Get();

            foreach (ManagementObject obj in mObject)
            {
                return obj.Properties["ProcessorId"].Value.ToString();
            }

            return "";
        }

        public static string GetPcHashInfo()
        {
            string cpuSerial = PcInfo.GetCPUSerial();
            string MotherBoardSerail = PcInfo.GetMotherBoardSerial();

            MD5 result = MD5.Create();
            byte[] data = result.ComputeHash(Encoding.UTF8.GetBytes(cpuSerial + MotherBoardSerail));
            var sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}
