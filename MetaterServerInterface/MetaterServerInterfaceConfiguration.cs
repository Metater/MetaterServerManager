using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using MetaMitStandard.Utils;

namespace MetaterServerInterface
{
    public class MetaterServerInterfaceConfiguration
    {
        public string serverInterfaceName;
        public IPAddress ip;
        public ushort port;
        public string pathToServerProcess;

        public MetaterServerInterfaceConfiguration()
        {
            ip = NetworkUtils.GetLocalIPv4();
        }

        private void CreateNewConfig()
        {
            Console.WriteLine("Enter the name for this server interface.");
            serverInterfaceName = Console.ReadLine();
            port = ConsoleUtils.ConsoleQuestions.AskPort();
        }

        public void LoadConfig()
        {
            // if loading fails, create a new config
        }
    }
}
