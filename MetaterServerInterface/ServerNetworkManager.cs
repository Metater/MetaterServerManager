using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using MetaMitStandard;
using MetaMitStandard.Client;
using MetaMitStandard.Utils;

namespace MetaterServerInterface
{
    public class ServerNetworkManager
    {
        private MSIConfig config;

        private MetaMitClient client;

        public ServerNetworkManager(Action connected)
        {
            client = new MetaMitClient();
            client.Connected += (object sender, ConnectedEventArgs e) =>
            {
                connected.Invoke();
            };
        }

        public void AddConfiguration(MSIConfig config)
        {
            this.config = config;
        }

        public void ConnectToController()
        {
            IPEndPoint ep = NetworkUtils.GetEndPoint(config.ip, config.port);
            client.Connect(ep);
        }

        public void SendData(string data)
        {
            client.Send(Encoding.ASCII.GetBytes(data));
        }

        public void Disconnect()
        {

        }
    }
}
