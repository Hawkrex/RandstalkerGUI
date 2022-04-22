using Newtonsoft.Json;
using RandstalkerGui.Models;
using RandstalkerGui.Tools;
using System.IO;

namespace RandstalkerGui.ViewModels.UserControls
{
    public class HomepageViewModel : BaseViewModel
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private RandstalkerApp randstalkerApp;


        public FileTreeViewModel PresetTreeViewModel { get; set; }
        public FileTreeViewModel PersonalSettingsTreeViewModel { get; set; }

        private string outputLog;

        public string OutputLog
        {
            get
            {
                return outputLog;

            }
            set
            {
                if (outputLog != value)

                {
                    Log.Debug($"{nameof(OutputLog)} => <{outputLog}> will change to <{value}>");

                    outputLog = value;

                    OnPropertyChanged();
                }
            }
        }

        private int progress;

        public int Progress
        {
            get
            {
                return progress;

            }
            set
            {
                if (progress != value)

                {
                    Log.Debug($"{nameof(Progress)} => <{progress}> will change to <{value}>");

                    progress = value;

                    OnPropertyChanged();
                }
            }
        }

        public RelayCommand GenerateRom { get { return new RelayCommand(_ => GenerateRomHandler()); } }

        private void GenerateRomHandler()
        {
            Log.Debug($"{nameof(GenerateRomHandler)}() => Command requested ...");

            Progress = 0;
            OutputLog = randstalkerApp.GenerateSeed(UserConfig.Instance.InputRomFilePath, UserConfig.Instance.OutputRomDirectoryPath, UserConfig.Instance.PresetsDirectoryPath + '/' + PresetTreeViewModel.SelectedFileRelativePath, UserConfig.Instance.PersonalSettingsDirectoryPath + '/' + PersonalSettingsTreeViewModel.SelectedFileRelativePath);

            Progress = 100;

            UserConfig.Instance.LastUsedPresetFilePath = PresetTreeViewModel.SelectedFileRelativePath;
            UserConfig.Instance.LastUsedPersonalSettingsFilePath = PersonalSettingsTreeViewModel.SelectedFileRelativePath;

            File.WriteAllText("Resources/userConfig.json", JsonConvert.SerializeObject(UserConfig.Instance));

            Log.Debug($"{nameof(GenerateRomHandler)}() => Command executed");
        }

        public HomepageViewModel()
        {
            PresetTreeViewModel = new FileTreeViewModel(UserConfig.Instance.PresetsDirectoryPath, UserConfig.Instance.LastUsedPresetFilePath, false);
            PersonalSettingsTreeViewModel = new FileTreeViewModel(UserConfig.Instance.PersonalSettingsDirectoryPath, UserConfig.Instance.LastUsedPersonalSettingsFilePath, false);

            randstalkerApp = new RandstalkerApp();

            Progress = 0;
        }
    }
}
