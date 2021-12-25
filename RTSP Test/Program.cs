using System;
using System.Diagnostics;

namespace RTSP_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "rtsp-simple-server",
                    Arguments = $" ",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };

            process.Start();
            string result = process.StandardOutput.ReadToEnd();
            
        }
    }
}
