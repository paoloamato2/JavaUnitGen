using System.Diagnostics;

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
                    //RedirectStandardError = true,
                    CreateNoWindow = true,
                    FileName = Exe,
                    Arguments = Input
                }
            };
            _process.Start();
            _process.WaitForExit();
            //if (process.HasExited)
            //{
            //    output = process.StandardOutput.ReadToEnd();
            //}
        }
    }
}