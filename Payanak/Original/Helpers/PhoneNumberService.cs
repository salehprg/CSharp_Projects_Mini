using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Helpers
{
    public class PhoneNumberService
    {
        public static string GetCorrectNumber(string input)
        {
            if (string.IsNullOrEmpty(input))
                return null;
            if (input.Length < 7)
                return null;
            if (input.Contains('-') && input.Length == 13)
                return input;
            var result = "";
            input = Reverse(input);
            var tmp = input.Substring(0, 4);
            result += "-" + Reverse(tmp);
            var code = input.Substring(8, 2);
            if (code == "15" || code == "12") // from mashhad or tehran
            {
                tmp = input.Substring(4, 4);
                result = "-" + Reverse(tmp) + result;
                tmp = "0" + Reverse(input.Substring(8, 2));
            }
            else // from other regions
            {
                tmp = input.Substring(4, 3);
                result = "-" + Reverse(tmp) + result;
                tmp = "0" + Reverse(input.Substring(7, 3));
            }
            result = tmp + result;
            return result;
        }
        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}