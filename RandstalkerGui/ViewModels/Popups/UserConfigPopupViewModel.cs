using Microsoft.WindowsAPICodePack.Dialogs;
using Newtonsoft.Json;
using RandstalkerGui.Models;
using RandstalkerGui.Tools;
using System.IO;

namespace RandstalkerGui.ViewModels.Popups
{
    public class UserConfigPopupViewModel : BaseViewModel
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public string RandstlakerExePath
        {
            get
            {
                return UserConfig.Instance.RandstlakerExePath;
            }
            set
            {
                if (UserConfig.Instance.RandstlakerExePath != value)
                {
                    Log.Debug($"{nameof(RandstlakerExePath)} => <{UserConfig.Instance.RandstlakerExePath}> will change to <{value}>");
                    UserConfig.Instance.RandstlakerExePath = value;
                    OnPropertyChanged();
                }
            }
        }

        public string PresetsPath
        {
            get
            {
                return UserConfig.Instance.PresetsPath;
            }
            set
            {
                if (UserConfig.Instance.PresetsPath != value)
                {
                    Log.Debug($"{nameof(PresetsPath)} => <{UserConfig.Instance.PresetsPath}> will change to <{value}>");
                    UserConfig.Instance.PresetsPath = value;
                    OnPropertyChanged();
                }
            }
        }

        public string PersonalSettingsPath
        {
            get
            {
                return UserConfig.Instance.PersonalSettingsPath;
            }
            set
            {
                if (UserConfig.Instance.PersonalSettingsPath != value)
                {
                    Log.Debug($"{nameof(PersonalSettingsPath)} => <{UserConfig.Instance.PersonalSettingsPath}> will change to <{value}>");
                    UserConfig.Instance.PersonalSettingsPath = value;
                    OnPropertyChanged();
                }
            }
        }

        public string InputRomPath
        {
            get
            {
                return UserConfig.Instance.InputRomPath;
            }
            set
            {
                if (UserConfig.Instance.InputRomPath != value)
                {
                    Log.Debug($"{nameof(InputRomPath)} => <{UserConfig.Instance.InputRomPath}> will change to <{value}>");
                    UserConfig.Instance.InputRomPath = value;
                    OnPropertyChanged();
                }
            }
        }

        public string OutputRomPath
        {
            get
            {
                return UserConfig.Instance.OutputRomPath;
            }
            set
            {
                if (UserConfig.Instance.OutputRomPath != value)
                {
                    Log.Debug($"{nameof(OutputRomPath)} => <{UserConfig.Instance.OutputRomPath}> will change to <{value}>");
                    UserConfig.Instance.OutputRomPath = value;
                    OnPropertyChanged();
                }
            }
        }

        public RelayCommand SelectRandstlakerExePath { get { return new RelayCommand(_ => SelectRandstlakerExePathHandler()); } }
        private void SelectRandstlakerExePathHandler()
        {
            Log.Debug($"{nameof(SelectRandstlakerExePathHandler)}() => Command requested ...");

            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = RandstlakerExePath;
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                RandstlakerExePath = dialog.FileName;

            Log.Debug($"{nameof(SelectRandstlakerExePathHandler)}() => Command executed");
        }

        public RelayCommand SelectPresetsPath { get { return new RelayCommand(_ => SelectPresetsPathHandler()); } }
        private void SelectPresetsPathHandler()
        {
            Log.Debug($"{nameof(SelectPresetsPathHandler)}() => Command requested ...");

            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = PresetsPath;
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                PresetsPath = dialog.FileName;

            Log.Debug($"{nameof(SelectPresetsPathHandler)}() => Command executed");
        }

        public RelayCommand SelectPersonalSettingsPath { get { return new RelayCommand(_ => SelectPersonalSettingsPathHandler()); } }
        private void SelectPersonalSettingsPathHandler()
        {
            Log.Debug($"{nameof(SelectPersonalSettingsPathHandler)}() => Command requested ...");

            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = PersonalSettingsPath;
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                PersonalSettingsPath = dialog.FileName;

            Log.Debug($"{nameof(SelectPersonalSettingsPathHandler)}() => Command executed");
        }

        public RelayCommand SelectInputRomPath { get { return new RelayCommand(_ => SelectInputRomPathHandler()); } }
        private void SelectInputRomPathHandler()
        {
            Log.Debug($"{nameof(SelectInputRomPathHandler)}() => Command requested ...");

            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = InputRomPath;
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                InputRomPath = dialog.FileName;

            Log.Debug($"{nameof(SelectInputRomPathHandler)}() => Command executed");
        }

        public RelayCommand SelectOutputRomPath { get { return new RelayCommand(_ => SelectOutputRomPathHandler()); } }
        private void SelectOutputRomPathHandler()
        {
            Log.Debug($"{nameof(SelectOutputRomPathHandler)}() => Command requested ...");

            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = OutputRomPath;
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                OutputRomPath = dialog.FileName;

            Log.Debug($"{nameof(SelectOutputRomPathHandler)}() => Command executed");
        }

        public RelayCommand SaveUserConfig { get { return new RelayCommand(_ => SaveUserConfigHandler()); } }
        private void SaveUserConfigHandler()
        {
            Log.Debug($"{nameof(SaveUserConfigHandler)}() => Command requested ...");

            File.WriteAllText("Resources/userConfig.json", JsonConvert.SerializeObject(UserConfig.Instance));

            Log.Debug($"{nameof(SaveUserConfigHandler)}() => Command executed");
        }
    }
}
