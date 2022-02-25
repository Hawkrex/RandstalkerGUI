using System.Diagnostics;
using System.IO;

namespace RandstalkerGui.Models
{
    public class RandstalkerApp
    {
        private ProcessStartInfo _start;

        public RandstalkerApp()
        {
            // Prepare the process to run
            _start = new ProcessStartInfo();

            // Enter the executable to run, including the complete path          
            _start.FileName = Path.GetFullPath(UserConfig.Instance.RandstlakerExePath + "randstalker.exe");
            // Do you want to show a console window?
            _start.WindowStyle = ProcessWindowStyle.Hidden;
            _start.RedirectStandardOutput = true;
            _start.RedirectStandardError = true;
            _start.CreateNoWindow = false;
            _start.UseShellExecute = false;
        }

        public string GenerateSeed(string inputRomPath, string outputRomPath, string preset, string personalSettings, string permalink = "")
        {
            // Enter in the command line arguments, everything you would enter after the executable name itself
            _start.Arguments = $"--inputrom={inputRomPath} --outputrom={outputRomPath} --preset={preset} --personalsettings={personalSettings} --nopause";
            if (!string.IsNullOrEmpty(permalink))
                _start.Arguments += " --permalink {permalink}";

            var proc = new Process();
            proc.StartInfo = _start;
            proc.Start();

            string output = proc.StandardOutput.ReadToEnd();
            string error = proc.StandardError.ReadToEnd();
            // Read the standard error of net.exe and write it on to console.                
            proc.WaitForExit();

            return output;
        }

    }
}
