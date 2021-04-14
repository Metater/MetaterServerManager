using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace MetaterServerInterface
{
    public class ServerProcessInterface
    {
        ProcessStartInfo processStartInfo;
        Process process;

        public ServerProcessInterface(string pathToProcess)
        {
            processStartInfo = new ProcessStartInfo();
            processStartInfo.FileName = pathToProcess;
            processStartInfo.RedirectStandardInput = true;
            processStartInfo.RedirectStandardOutput = true;
            processStartInfo.RedirectStandardError = true;
            processStartInfo.UseShellExecute = false;
        }

        public bool Start()
        {
            try
            {
                process = Process.Start(processStartInfo);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public void Listen(Action<string> outputDataReceived, Action<string> errorDataReceived)
        {
            process.OutputDataReceived += (object sender, DataReceivedEventArgs e) =>
            {
                outputDataReceived?.Invoke(e.Data);
            };
            process.ErrorDataReceived += (object sender, DataReceivedEventArgs e) =>
            {
                errorDataReceived?.Invoke(e.Data);
            };
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
        }

        public void SendCommand(string command)
        {
            process.StandardInput.WriteLine(command);
            process.StandardInput.Flush();
        }

        public void Close()
        {
            process.StandardInput.Close();
        }

        public void WaitForExit()
        {
            process.WaitForExit();
        }
    }
}
