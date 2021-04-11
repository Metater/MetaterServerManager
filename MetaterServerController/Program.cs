using System;
using MetaMitStandard.Utils;
using System.Threading;
using MetaMitStandard;
using System.Text;

namespace MetaterServerController
{
    class Program
    {
        static void Main(string[] args)
        {
            MetaMitServer server = new MetaMitServer(5001, 100);

            Console.WriteLine("Listening on: " + server.ep);

            server.ClientConnected += Server_ClientConnected;
            server.DataReceived += (object sender, MetaMitStandard.Server.DataReceivedEventArgs e) =>
            {
                server.BroadcastToBut(e.clientConnection, e.data);
                Console.WriteLine(e.clientConnection.guid + ": " + Encoding.ASCII.GetString(e.data));
            };

            server.Start();

            // !Console.KeyAvailable
            // true
            while (true)
            {
                server.PollEvents();
                Thread.Sleep(1);
            }
        }

        private static void Server_ClientConnected(object sender, MetaMitStandard.Server.ClientConnectedEventArgs e)
        {
            Console.WriteLine("Client Connected: \n" + "\tGuid: " + e.guid + "\n" + "\tEndpoint: " + e.ep);
        }
    }
}
