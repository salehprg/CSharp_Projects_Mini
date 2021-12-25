using System;


namespace TestFarazSms
{
    class Program
    {
        static void Main(string[] args)
        {
            FarazSmsApi.SendSms(new string[]{"9154807673"} , "Hello" , "9878899");
        }
    }
}
