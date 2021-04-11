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
            Console.WriteLine("Enter the server executable's name.");
            string name = Console.ReadLine();
            if (!File.Exists(Directory.GetCurrentDirectory() + @"\" + name))
            {
                Console.WriteLine("File not found.");
                return;
            }

            var processInfo = new ProcessStartInfo(Directory.GetCurrentDirectory() + @"\" + name);

            processInfo.RedirectStandardInput = true;
            processInfo.RedirectStandardOutput = true;
            processInfo.RedirectStandardError = true;

            processInfo.UseShellExecute = false;

            //processInfo.CreateNoWindow = true;

            var process = Process.Start(processInfo);

            process.OutputDataReceived += (object sender, DataReceivedEventArgs e) =>
            {
                Console.WriteLine(e.Data);
            };
            process.BeginOutputReadLine();

            process.ErrorDataReceived += (object sender, DataReceivedEventArgs e) =>
            {
                Console.WriteLine(e.Data);
            };
            process.BeginErrorReadLine();

            process.StandardInput.WriteLine("save-all");
            process.StandardInput.Flush();
            Thread.Sleep(5000);
            process.StandardInput.WriteLine("stop");
            process.StandardInput.Flush();
            //process.StandardInput.Close();

            //process.WaitForExit();

            Console.ReadLine();
        }
    }
}
