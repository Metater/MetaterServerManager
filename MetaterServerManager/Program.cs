using System;
using MetaMitStandard;
using MetaMitStandard.Utils;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MetaterServerManager
{
    class Program
    {
        static void Main(string[] args)
        {
            MetaMitClient client = new MetaMitClient();

            IPAddress ip = ConsoleUtils.ConsoleQuestions.AskIP();
            ushort port = ConsoleUtils.ConsoleQuestions.AskPort();
            string username = ConsoleUtils.ConsoleQuestions.AskUsername();

            IPEndPoint ep = NetworkUtils.GetEndPoint(ip, port);

            client.Connected += Client_Connected;
            client.DataReceived += Client_DataReceived;

            client.Connect(ep);

            Thread.Sleep(100);

            Thread listeningThread = new Thread(new ThreadStart(() =>
            {
                while (true)
                {
                    client.PollEvents();
                    Thread.Sleep(5);
                }
            }));
            listeningThread.Start();

            while (true)
            {
                string message = Console.ReadLine();
                if (!string.IsNullOrEmpty(message) && !string.IsNullOrWhiteSpace(message))
                {
                    client.Send(Encoding.ASCII.GetBytes(username + ": " + message));
                }
            }
        }

        private static void Client_Connected(object sender, MetaMitStandard.Client.ConnectedEventArgs e)
        {
            Console.WriteLine("Connected to server!");
        }

        private static void Client_DataReceived(object sender, MetaMitStandard.Client.DataReceivedEventArgs e)
        {
            Console.WriteLine(Encoding.ASCII.GetString(e.data));
        }
    }
}
