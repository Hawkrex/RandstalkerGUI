using System.Diagnostics;
using System.IO;

namespace RandstalkerGui.Models
{
    public class RandstalkerApp
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private ProcessStartInfo startInfos;

        public RandstalkerApp()
        {
            startInfos = new ProcessStartInfo();

            startInfos.FileName = UserConfig.Instance.RandstlakerExeFilePath;
            startInfos.WindowStyle = ProcessWindowStyle.Hidden;
            startInfos.RedirectStandardOutput = true;
            startInfos.RedirectStandardError = true;
            startInfos.CreateNoWindow = false;
            startInfos.UseShellExecute = false;
            startInfos.WorkingDirectory = Path.GetDirectoryName(UserConfig.Instance.RandstlakerExeFilePath);
        }

        public string GenerateSeed(string inputRomFilePath, string outputRomDirectoryPath, string presetRelativeFilePath, string personalSettingsFilePath, string permalink, string outputFileName)
        {
            string outputRomPath = string.IsNullOrEmpty(outputFileName) ? outputRomDirectoryPath : Path.Combine(outputRomDirectoryPath, outputFileName + ".md");

            // Enter in the command line arguments, everything you would enter after the executable name itself
            startInfos.Arguments = $"--inputrom={inputRomFilePath} --outputrom={outputRomPath} --preset={presetRelativeFilePath} --personalsettings={personalSettingsFilePath} --nopause";

            string log = $"Generating seed with inputrom={inputRomFilePath}, outputrom={outputRomPath}, preset={presetRelativeFilePath}, personalsettings={personalSettingsFilePath}";
            if (!string.IsNullOrEmpty(permalink))
            {
                startInfos.Arguments += $" --permalink={permalink}";
                log += $", permalink={permalink}";
            }

            var randstalkerProcess = new Process();
            randstalkerProcess.StartInfo = startInfos;

            Log.Info(log);
            randstalkerProcess.Start();

            string output = randstalkerProcess.StandardOutput.ReadToEnd();
            string error = randstalkerProcess.StandardError.ReadToEnd();

            // Read the standard error of net.exe and write it on to console.                
            randstalkerProcess.WaitForExit();

            if (string.IsNullOrEmpty(error))
            {
                return output;
            }
            else
            {
                return $"{log} failed :\n{error}";
            }
        }
    }
}
