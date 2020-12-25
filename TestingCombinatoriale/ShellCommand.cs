using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace TestingCombinatoriale
{
    public class ShellCommand
    {
        private Process _process;
        public string Exe;
        public string Input;
        public string Output;

        public ShellCommand(string input)
        {
            Input = input;
            Exe = "cmd.exe";
        }

        public void ExecuteCommand()
        {
            _process = new Process
            {
                StartInfo =
                {
                    UseShellExecute = false,
                    //RedirectStandardOutput = true,
                   // RedirectStandardError = true,
                    CreateNoWindow = true,
                    FileName = Exe,
                    Arguments = Input
                }
            };
            _process.Start();
            _process.WaitForExit();
            StringBuilder sb = new StringBuilder();
            //string inputcommand = Input.Replace("/C", "");
            //File.AppendAllText(Directory.GetCurrentDirectory() + @"\log.txt", "Input: "+Input+Environment.NewLine);
            //while (!_process.StandardOutput.EndOfStream)
            //{
            //    string line = _process.StandardOutput.ReadLine()+Environment.NewLine;
            //    File.AppendAllText(Directory.GetCurrentDirectory()+@"\log.txt",line);
            //}
            //File.AppendAllText(Directory.GetCurrentDirectory()+@"\log.txt",_process.StandardOutput.ReadToEnd());
            //if (process.HasExited)
            //{
            //    output = process.StandardOutput.ReadToEnd();
            //}
        }
    }
}