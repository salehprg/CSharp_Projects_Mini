using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using RtspClientSharp;
using RtspClientSharp.RawFrames;
using RtspClientSharp.Rtsp;

namespace IP_Camera
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string localip = "192.168.1.";
                
                for(int i = 0; i < 256;i++)
                {
                    string ip = localip + i.ToString();

                    var serverUri = new Uri("rtsp://" + ip);
                    var connectionParameters = new ConnectionParameters(serverUri);
                    var cancellationTokenSource = new CancellationTokenSource();

                    if(ConnectAsync(connectionParameters, cancellationTokenSource.Token).Result)
                    {
                        Console.WriteLine(ip);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        private static async Task<bool> ConnectAsync(ConnectionParameters connectionParameters, CancellationToken token)
        {
            try
            {
                var rtspClient = new RtspClient(connectionParameters);
                rtspClient.ConnectionParameters.ConnectTimeout = TimeSpan.FromMilliseconds(100);

                await rtspClient.ConnectAsync(token);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private static void PingCompletedEvent(object sender, PingCompletedEventArgs e)
        {
            string ip = (string)e.UserState;

            if(e.Reply != null && e.Reply.Status == IPStatus.Success)
            {
                Console.WriteLine(ip);
            }

        }
    }
}