using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using MetaMitStandard.Utils;

namespace MetaterServerInterface
{
    public class MSIConfig
    {
        public string serverInterfaceName;
        public IPAddress ip;
        public ushort port;
        public string pathToServerProcess;

        public MSIConfig()
        {
            ip = NetworkUtils.GetLocalIPv4();
        }

        private void CreateConfig()
        {
            Console.WriteLine("Enter the name for this server interface.");
            serverInterfaceName = Console.ReadLine();
            ip = ConsoleUtils.ConsoleQuestions.AskIP();
            port = ConsoleUtils.ConsoleQuestions.AskPort();
            pathToServerProcess = ConsoleUtils.ConsoleQuestions.AskQuestionString("Enter the local path to the server process.", (answer) => {
                string suggestedPath = Directory.GetCurrentDirectory() + @"\" + answer;
                return (File.Exists(suggestedPath), suggestedPath);
            });
            SaveConfig();
        }

        private void SaveConfig()
        {
            string pathToConfigFile = Directory.GetCurrentDirectory() + @"\MetaterServerInterface.conf";
            List<string> configData = new List<string>();
            configData.Add(serverInterfaceName);
            configData.Add(ip.ToString());
            configData.Add(port.ToString());
            configData.Add(pathToServerProcess);
            File.WriteAllLines(pathToConfigFile, configData);
        }

        public void LoadConfig()
        {
            string pathToConfigFile = Directory.GetCurrentDirectory() + @"\MetaterServerInterface.conf";
            if (!File.Exists(pathToConfigFile))
            {
                CreateConfig();
            }
            else
            {
                Console.WriteLine("Config loaded, delete config and relaunch to change parameters.");
            }

            bool configLoaded = false;
            while (!configLoaded)
            {
                try
                {
                    string[] config = File.ReadAllLines(pathToConfigFile);
                    serverInterfaceName = config[0];
                    ip = IPAddress.Parse(config[1]);
                    port = Convert.ToUInt16(config[2]);
                    pathToServerProcess = config[3];
                    configLoaded = true;
                }
                catch (Exception)
                {
                    Console.WriteLine("Failed to load config, create a new one.");
                    CreateConfig();
                }
            }
        }
    }
}
