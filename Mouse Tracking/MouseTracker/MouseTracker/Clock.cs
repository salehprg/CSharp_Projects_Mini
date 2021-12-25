using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Yort.Ntp;

namespace MouseTracker
{
    public class Clock
    {
        static double offset = 0 ;
        DateTime currentTime;

        public double GetOffset()
        {
            var client = new NtpClient("0.ir.pool.ntp.org");
            client.TimeReceived += Client_TimeReceived;
            client.ErrorOccurred += Client_ErrorOccurred;
            client.BeginRequestTime();

            return offset;
        }
        public DateTime GetNTPTime()
        {
            var client = new NtpClient("0.ir.pool.ntp.org");
            client.TimeReceived += Client_TimeReceived;
            client.ErrorOccurred += Client_ErrorOccurred;
            client.BeginRequestTime();

            return DateTime.Now.AddMilliseconds(offset);
        }

        public DateTime getTimeNow()
        {
            return DateTime.Now.AddMilliseconds(offset);
        }


        private void Client_ErrorOccurred(object sender, NtpNetworkErrorEventArgs e)
        {
            offset = -1;
        }

        private void Client_TimeReceived(object sender, NtpTimeReceivedEventArgs e)
        {
            offset = (e.CurrentTime - e.ReceivedAt).TotalMilliseconds;
        }
    }

}
