using System;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace MetaterServerInterface
{
    class Program
    {
        static void Main(string[] args)
        {
            MSIConfig config = new MSIConfig();
            ServerProcessManager processManager;


            Console.WriteLine("Enter the server executable's name.");
            string name = Console.ReadLine();
            if (!File.Exists(Directory.GetCurrentDirectory() + @"\" + name))
            {
                Console.WriteLine("File not found.");
                return;
            }

            config.LoadConfig();

            Console.ReadLine();
        }
    }
}
