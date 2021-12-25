using System;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Threading;

namespace ConsoleModbus
{
    class Program
    {
        static void Main(string[] args)
        {
            string port = "";
            int timeOut = 100;
            int slaveId = 0;
            int baud = 0;

            bool read = false;

            if(RuntimeInformation.IsOSPlatform(OSPlatform.Linux))    
            {
                Console.WriteLine("Port : ");
                port = Console.ReadLine();

                Console.WriteLine("BaudRate : ");
                baud = int.Parse(Console.ReadLine());

                Console.WriteLine("Timeout : ");
                timeOut = int.Parse(Console.ReadLine());

                Console.WriteLine("Slave Id : ");
                slaveId = int.Parse(Console.ReadLine());

                Console.WriteLine("Just Read ? (true/false) : ");
                read = bool.Parse(Console.ReadLine());
            }
            else
            {
                port = "COM7";
            }

            Modbus modbus = new Modbus();
            modbus.Open(port , baud , 8 , Parity.None , StopBits.One);
            modbus.timeOut = timeOut;
            
            short[] datas = new short[5];
            short[] dataWrite = new short[5];
            
            bool result = false;

            int i = 0;

            while(true)
            {
                try
                {
                    
                    Console.WriteLine(modbus.modbusStatus);

                    dataWrite[0] = (short)(i+2);
                    dataWrite[1] = (short)(i+3);
                    dataWrite[2] = (short)(i+4);
                    dataWrite[3] = (short)(i+5);
                    dataWrite[4] = (short)(i+6);

                    if(!read)
                    {
                        result = modbus.WriteMulipleRegister(Convert.ToByte(slaveId) , 0 , 5 , dataWrite);
                        Console.WriteLine(modbus.modbusStatus);
                    }

                    result = modbus.ReadHoldingRegister(Convert.ToByte(slaveId) , 0 , 5 , ref datas);
                    Console.WriteLine(modbus.modbusStatus);

                    if(result)
                    {
                        Console.WriteLine("0 = " + datas[0]);
                        Console.WriteLine("1 = " + datas[1]);
                        Console.WriteLine("2 = " + datas[2]);
                        Console.WriteLine("3 = " + datas[3]);
                        Console.WriteLine("4 = " + datas[4]);
                    }
                    else
                    {
                        Console.WriteLine(result);
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                
                Thread.Sleep(500);
                i++;
            }
            modbus.Close();
            result = false;
            
        }
    }

}
