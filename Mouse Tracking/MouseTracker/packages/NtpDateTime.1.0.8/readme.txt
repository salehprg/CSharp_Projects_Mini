-----------------------------------------------
Ussage:
-----------------------------------------------
To get current DateTime using NTP protocol:

using System;
using Tarczynski.NtpDateTime;

namespace NtpDateTime.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var ntpDateTime = DateTime.Now.FromNtp();
            Console.WriteLine(ntpDateTime);
            
            Console.ReadLine();
        }
    }
}



---
Best regards
Dariusz Tarczynski