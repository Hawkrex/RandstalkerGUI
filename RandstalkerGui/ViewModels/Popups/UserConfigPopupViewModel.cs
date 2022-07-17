using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using Newtonsoft.Json;
using RandstalkerGui.Models;
using RandstalkerGui.Tools;
using System.IO;
using System.Windows;

namespace RandstalkerGui.ViewModels.Popups
{
    public class UserConfigPopupViewModel : BaseViewModel
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public string RandstlakerExeFilePath
        {
            get
            {
                return UserConfig.Instance.RandstlakerExeFilePath;
            }
            set
            {
                if (UserConfig.Instance.RandstlakerExeFilePath != value)
                {
                    Log.Debug($"{nameof(RandstlakerExeFilePath)} => <{UserConfig.Instance.RandstlakerExeFilePath}> will change to <{value}>");
                    UserConfig.Instance.RandstlakerExeFilePath = value;
                    OnPropertyChanged();
                }
            }
        }

        public string PresetsDirectoryPath
        {
            get
            {
                return UserConfig.Instance.PresetsDirectoryPath;
            }
            set
            {
                if (UserConfig.Instance.PresetsDirectoryPath != value)
                {
                    Log.Debug($"{nameof(PresetsDirectoryPath)} => <{UserConfig.Instance.PresetsDirectoryPath}> will change to <{value}>");
                    UserConfig.Instance.PresetsDirectoryPath = value;
                    OnPropertyChanged();
                }
            }
        }

        public string PersonalSettingsDirectoryPath
        {
            get
            {
                return UserConfig.Instance.PersonalSettingsDirectoryPath;
            }
            set
            {
                if (UserConfig.Instance.PersonalSettingsDirectoryPath != value)
                {
                    Log.Debug($"{nameof(PersonalSettingsDirectoryPath)} => <{UserConfig.Instance.PersonalSettingsDirectoryPath}> will change to <{value}>");
                    UserConfig.Instance.PersonalSettingsDirectoryPath = value;
                    OnPropertyChanged();
                }
            }
        }

        public string InputRomFilePath
        {
            get
            {
                return UserConfig.Instance.InputRomFilePath;
            }
            set
            {
                if (UserConfig.Instance.InputRomFilePath != value)
                {
                    Log.Debug($"{nameof(InputRomFilePath)} => <{UserConfig.Instance.InputRomFilePath}> will change to <{value}>");
                    UserConfig.Instance.InputRomFilePath = value;
                    OnPropertyChanged();
                }
            }
        }

        public string OutputRomDirectoryPath
        {
            get
            {
                return UserConfig.Instance.OutputRomDirectoryPath;
            }
            set
            {
                if (UserConfig.Instance.OutputRomDirectoryPath != value)
                {
                    Log.Debug($"{nameof(OutputRomDirectoryPath)} => <{UserConfig.Instance.OutputRomDirectoryPath}> will change to <{value}>");
                    UserConfig.Instance.OutputRomDirectoryPath = value;
                    OnPropertyChanged();
                }
            }
        }

        public RelayCommand SelectRandstlakerExeFilePath { get { return new RelayCommand(_ => SelectRandstlakerExeFilePathHandler()); } }

        private void SelectRandstlakerExeFilePathHandler()
        {
            Log.Debug($"{nameof(SelectRandstlakerExeFilePathHandler)}() => Command requested ...");

            var dialog = new OpenFileDialog();
            dialog.InitialDirectory = RandstlakerExeFilePath;
            dialog.Filter = "Exe file (*.exe)|*.exe";

            if (dialog.ShowDialog() == true)
            {
                RandstlakerExeFilePath = dialog.FileName;
            }

            Log.Debug($"{nameof(SelectRandstlakerExeFilePathHandler)}() => Command executed");
        }

        public RelayCommand SelectPresetsDirectoryPath { get { return new RelayCommand(_ => SelectPresetsDirectoryPathHandler()); } }

        private void SelectPresetsDirectoryPathHandler()
        {
            Log.Debug($"{nameof(SelectPresetsDirectoryPathHandler)}() => Command requested ...");

            var dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = PresetsDirectoryPath;
            dialog.IsFolderPicker = true;

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                PresetsDirectoryPath = dialog.FileName;
            }

            Log.Debug($"{nameof(SelectPresetsDirectoryPathHandler)}() => Command executed");
        }

        public RelayCommand SelectPersonalSettingsDirectoryPath { get { return new RelayCommand(_ => SelectPersonalSettingsDirectoryPathHandler()); } }

        private void SelectPersonalSettingsDirectoryPathHandler()
        {
            Log.Debug($"{nameof(SelectPersonalSettingsDirectoryPathHandler)}() => Command requested ...");

            var dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = PersonalSettingsDirectoryPath;
            dialog.IsFolderPicker = true;

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                PersonalSettingsDirectoryPath = dialog.FileName;
            }

            Log.Debug($"{nameof(SelectPersonalSettingsDirectoryPathHandler)}() => Command executed");
        }

        public RelayCommand SelectInputRomFilePath { get { return new RelayCommand(_ => SelectInputRomFilePathHandler()); } }

        private void SelectInputRomFilePathHandler()
        {
            Log.Debug($"{nameof(SelectInputRomFilePathHandler)}() => Command requested ...");

            var dialog = new OpenFileDialog();
            dialog.Filter = "Megadrive file (*.md)|*.md";
            if (File.Exists(InputRomFilePath))
            {
                dialog.InitialDirectory = Path.GetDirectoryName(InputRomFilePath);
            }
            
            if (dialog.ShowDialog().Value)
            {
                InputRomFilePath = dialog.FileName;
            }

            Log.Debug($"{nameof(SelectInputRomFilePathHandler)}() => Command executed");
        }

        public RelayCommand SelectOutputRomDirectoryPath { get { return new RelayCommand(_ => SelectOutputRomDirectoryPathHandler()); } }

        private void SelectOutputRomDirectoryPathHandler()
        {
            Log.Debug($"{nameof(SelectOutputRomDirectoryPathHandler)}() => Command requested ...");

            var dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = OutputRomDirectoryPath;
            dialog.IsFolderPicker = true;

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                OutputRomDirectoryPath = dialog.FileName;
            }

            Log.Debug($"{nameof(SelectOutputRomDirectoryPathHandler)}() => Command executed");
        }

        public RelayCommand<Window> SaveUserConfig { get { return new RelayCommand<Window>(dialog => SaveUserConfigHandler(dialog)); } }

        private void SaveUserConfigHandler(Window dialog)
        {
            Log.Debug($"{nameof(SaveUserConfigHandler)}() => Command requested ...");

            File.WriteAllText("Resources/userConfig.json", JsonConvert.SerializeObject(UserConfig.Instance));

            if (dialog != null)
            {
                dialog.DialogResult = true;
            }

            Log.Debug($"{nameof(SaveUserConfigHandler)}() => Command executed");
        }
    }
}
