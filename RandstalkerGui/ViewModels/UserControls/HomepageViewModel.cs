using Newtonsoft.Json;
using RandstalkerGui.Models;
using RandstalkerGui.Tools;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;

namespace RandstalkerGui.ViewModels.UserControls
{
    public class HomepageViewModel : BaseViewModel
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        const string permalinkMark = "Permalink: ";

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

        private string permalinkToGenerateFrom;
        public string PermalinkToGenerateFrom
        {
            get
            {
                return permalinkToGenerateFrom;
            }
            set
            {
                if (permalinkToGenerateFrom != value)
                {
                    Log.Debug($"{nameof(PermalinkToGenerateFrom)} => <{permalinkToGenerateFrom}> will change to <{value}>");
                    permalinkToGenerateFrom = value;
                    OnPropertyChanged();
                }
            }
        }

        private string outputRomFileName;
        public string OutputRomFileName
        {
            get
            {
                return outputRomFileName;
            }
            set
            {
                if (outputRomFileName != value)
                {
                    Log.Debug($"{nameof(OutputRomFileName)} => <{outputRomFileName}> will change to <{value}>");
                    outputRomFileName = value;
                    OnPropertyChanged();
                }
            }
        }

        private string permalinkToCopy;
        public string PermalinkToCopy
        {
            get
            {
                return permalinkToCopy;
            }
            set
            {
                if (permalinkToCopy != value)
                {
                    Log.Debug($"{nameof(PermalinkToCopy)} => <{permalinkToCopy}> will change to <{value}>");
                    permalinkToCopy = value;
                    OnPropertyChanged();
                }
            }
        }

        public RelayCommand GenerateRom { get { return new RelayCommand(_ => GenerateRomHandler()); } }
        private void GenerateRomHandler()
        {
            Log.Debug($"{nameof(GenerateRomHandler)}() => Command requested ...");

            Progress = 0;
            OutputLog = randstalkerApp.GenerateSeed(UserConfig.Instance.InputRomFilePath,
                UserConfig.Instance.OutputRomDirectoryPath,
                Path.Combine(UserConfig.Instance.PresetsDirectoryPath, PresetTreeViewModel.SelectedFileRelativePath),
                Path.Combine(UserConfig.Instance.PersonalSettingsDirectoryPath, PersonalSettingsTreeViewModel.SelectedFileRelativePath),
                PermalinkToGenerateFrom,
                OutputRomFileName);

            PermalinkToCopy = Regex.Match(OutputLog, @"Permalink: (.*)").Groups[1].Value;
            Progress = 100;

            UserConfig.Instance.LastUsedPresetFilePath = PresetTreeViewModel.SelectedFileRelativePath;
            UserConfig.Instance.LastUsedPersonalSettingsFilePath = PersonalSettingsTreeViewModel.SelectedFileRelativePath;

            File.WriteAllText("Resources/userConfig.json", JsonConvert.SerializeObject(UserConfig.Instance));

            Log.Debug($"{nameof(GenerateRomHandler)}() => Command executed");
        }

        public RelayCommand CopyPermalink { get { return new RelayCommand(_ => CopyPermalinkHandler()); } }
        private void CopyPermalinkHandler()
        {
            Log.Debug($"{nameof(CopyPermalinkHandler)}() => Command requested ...");

            Clipboard.SetText(PermalinkToCopy);

            Log.Debug($"{nameof(CopyPermalinkHandler)}() => Command executed");
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
