using System.Diagnostics;
using System.IO;

namespace RandstalkerGui.Models
{
    public class RandstalkerApp
    {
        private ProcessStartInfo start;


        public RandstalkerApp()
        {
            // Prepare the process to run
            start = new ProcessStartInfo();


            // Enter the executable to run, including the complete path          
            start.FileName = Path.GetFullPath(UserConfig.Instance.RandstlakerExeDirectoryPath + "/randstalker.exe");

            // Do you want to show a console window?
            start.WindowStyle = ProcessWindowStyle.Hidden;

            start.RedirectStandardOutput = true;

            start.RedirectStandardError = true;

            start.CreateNoWindow = false;

            start.UseShellExecute = false;

        }

        public string GenerateSeed(string inputRomFilePath, string outputRomDirectoryPath, string presetFilePath, string personalSettingsFilePath, string permalink = "")
        {
            // Enter in the command line arguments, everything you would enter after the executable name itself
            start.Arguments = $"--inputrom={inputRomFilePath} --outputrom={outputRomDirectoryPath} --preset={presetFilePath} --personalsettings={personalSettingsFilePath} --nopause";

            if (!string.IsNullOrEmpty(permalink))
                start.Arguments += " --permalink {permalink}";

            var proc = new Process();
            proc.StartInfo = start;

            proc.Start();

            string output = proc.StandardOutput.ReadToEnd();
            string error = proc.StandardError.ReadToEnd();
            // Read the standard error of net.exe and write it on to console.                
            proc.WaitForExit();

            return output;
        }

    }
}
