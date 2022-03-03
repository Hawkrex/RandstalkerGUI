using RandstalkerGui.Models;
using RandstalkerGui.Tools;

namespace RandstalkerGui.ViewModels.UserControls
{
    public class HomepageViewModel : BaseViewModel
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private RandstalkerApp _randstalkerApp;

        private string _outputLog;
        public string OutputLog
        {
            get
            {
                return _outputLog;
            }
            set
            {
                if (_outputLog != value)
                {
                    Log.Debug($"{nameof(OutputLog)} => <{_outputLog}> will change to <{value}>");
                    _outputLog = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _progress;
        public int Progress
        {
            get
            {
                return _progress;
            }
            set
            {
                if (_progress != value)
                {
                    Log.Debug($"{nameof(Progress)} => <{_progress}> will change to <{value}>");
                    _progress = value;
                    OnPropertyChanged();
                }
            }
        }

        public RelayCommand GenerateRom { get { return new RelayCommand(_ => GenerateRomHandler()); } }
        private void GenerateRomHandler()
        {
            Log.Debug($"{nameof(GenerateRomHandler)}() => Command requested ...");

            Progress = 0;
            OutputLog = _randstalkerApp.GenerateSeed(UserConfig.Instance.InputRomFilePath, UserConfig.Instance.OutputRomDirectoryPath, UserConfig.Instance.PresetsDirectoryPath + UserConfig.Instance.DefaultPresetFilePath, UserConfig.Instance.PersonalSettingsDirectoryPath + UserConfig.Instance.DefaultPersonalSettingsFilePath);
            Progress = 100;

            Log.Debug($"{nameof(GenerateRomHandler)}() => Command executed");
        }

        public HomepageViewModel()
        {
            _randstalkerApp = new RandstalkerApp();

            Progress = 0;
        }
    }
}
