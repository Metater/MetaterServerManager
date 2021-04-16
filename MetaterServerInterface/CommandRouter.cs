using System;
using System.Collections.Generic;
using System.Text;

namespace MetaterServerInterface
{
    public class CommandRouter
    {
        private Dictionary<string, Action<string>> commandRoutes = new Dictionary<string, Action<string>>();

        public void RegisterRoute(string commandHeader, Action<string> commandHandler)
        {
            commandRoutes.Add(commandHeader, commandHandler);
        }

        public void Route(string command)
        {
            string[] commandArgs = command.Split(" ");
            string commandHeader = commandArgs[0];
            if (commandRoutes.TryGetValue(commandHeader, out Action<string> commandHandler))
            {
                commandHandler.Invoke(command);
            }
            else
            {
                Console.WriteLine("Unrecognised command header: " + commandHeader);
            }
        }
    }
}
