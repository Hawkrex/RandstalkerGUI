using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using Newtonsoft.Json;
using RandstalkerGui.Models;
using System;
using System.IO;
using System.Windows;

namespace RandstalkerGui.ViewModels.Popups
{
    public class UserConfigPopupViewModel : ObservableObject
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private bool isModificationFromOpenDialog = false;

        public string RandstlakerExeFilePath
        {
            get => UserConfig.Instance.RandstlakerExeFilePath;
            set
            {
                if (UserConfig.Instance.RandstlakerExeFilePath != value || isModificationFromOpenDialog)
                {
                    OnPropertyChanging();
                    UserConfig.Instance.RandstlakerExeFilePath = value;
                    OnPropertyChanged();
                }
            }
        }

        public string PresetsDirectoryPath
        {
            get => UserConfig.Instance.PresetsDirectoryPath;
            set
            {
                if (UserConfig.Instance.PresetsDirectoryPath != value || isModificationFromOpenDialog)
                {
                    OnPropertyChanging();
                    UserConfig.Instance.PresetsDirectoryPath = value;
                    OnPropertyChanged();
                }
            }
        }

        public string PersonalSettingsDirectoryPath
        {
            get => UserConfig.Instance.PersonalSettingsDirectoryPath;
            set
            {
                if (UserConfig.Instance.PersonalSettingsDirectoryPath != value || isModificationFromOpenDialog)
                {
                    OnPropertyChanging();
                    UserConfig.Instance.PersonalSettingsDirectoryPath = value;
                    OnPropertyChanged();
                }
            }
        }

        public string InputRomFilePath
        {
            get => UserConfig.Instance.InputRomFilePath;
            set
            {
                if (UserConfig.Instance.InputRomFilePath != value || isModificationFromOpenDialog)
                {
                    OnPropertyChanging();
                    UserConfig.Instance.InputRomFilePath = value;
                    OnPropertyChanged();
                }
            }
        }

        public string OutputRomDirectoryPath
        {
            get => UserConfig.Instance.OutputRomDirectoryPath;
            set
            {
                if (UserConfig.Instance.OutputRomDirectoryPath != value || isModificationFromOpenDialog)
                {
                    OnPropertyChanging();
                    UserConfig.Instance.OutputRomDirectoryPath = value;
                    OnPropertyChanged();
                }
            }
        }

        public RelayCommand SelectRandstlakerExeFilePath => new(SelectRandstlakerExeFilePathHandler);

        private void SelectRandstlakerExeFilePathHandler()
        {
            var dialog = new OpenFileDialog
            {
                InitialDirectory = RandstlakerExeFilePath,
                Filter = "Exe file (*.exe)|*.exe"
            };

            if (dialog.ShowDialog() == true)
            {
                isModificationFromOpenDialog = true;
                RandstlakerExeFilePath = dialog.FileName;
                isModificationFromOpenDialog = false;
            }
        }

        public RelayCommand SelectPresetsDirectoryPath => new(SelectPresetsDirectoryPathHandler);

        private void SelectPresetsDirectoryPathHandler()
        {
            var dialog = new CommonOpenFileDialog
            {
                InitialDirectory = PresetsDirectoryPath,
                IsFolderPicker = true
            };

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                isModificationFromOpenDialog = true;
                PresetsDirectoryPath = dialog.FileName;
                isModificationFromOpenDialog = false;
            }
        }

        public RelayCommand SelectPersonalSettingsDirectoryPath => new(SelectPersonalSettingsDirectoryPathHandler);

        private void SelectPersonalSettingsDirectoryPathHandler()
        {
            var dialog = new CommonOpenFileDialog
            {
                InitialDirectory = PersonalSettingsDirectoryPath,
                IsFolderPicker = true
            };

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                isModificationFromOpenDialog = true;
                PersonalSettingsDirectoryPath = dialog.FileName;
                isModificationFromOpenDialog = false;
            }
        }

        public RelayCommand SelectInputRomFilePath => new(SelectInputRomFilePathHandler);

        private void SelectInputRomFilePathHandler()
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Megadrive file (*.md)|*.md"
            };

            if (File.Exists(InputRomFilePath))
            {
                dialog.InitialDirectory = Path.GetDirectoryName(InputRomFilePath);
            }

            if (dialog.ShowDialog().Value)
            {
                isModificationFromOpenDialog = true;
                InputRomFilePath = dialog.FileName;
                isModificationFromOpenDialog = false;
            }
        }

        public RelayCommand SelectOutputRomDirectoryPath => new(SelectOutputRomDirectoryPathHandler);

        private void SelectOutputRomDirectoryPathHandler()
        {
            var dialog = new CommonOpenFileDialog
            {
                InitialDirectory = OutputRomDirectoryPath,
                IsFolderPicker = true
            };

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                isModificationFromOpenDialog = true;
                OutputRomDirectoryPath = dialog.FileName;
                isModificationFromOpenDialog = false;
            }
        }

        public RelayCommand<Window> SaveUserConfig => new(SaveUserConfigHandler);

        private void SaveUserConfigHandler(Window dialog)
        {
            try
            {
                UserConfig.SaveFile();

                MessageBox.Show((string)App.Instance.TryFindResource("FileWriteSuccessMessage"), (string)App.Instance.TryFindResource("SuccessTitle"), MessageBoxButton.OK, MessageBoxImage.Information);
                dialog.Close();

                UserConfig.NotifySavedValidUserConfig();
            }
            catch (Exception ex)
            {
                string errorMessage = (string)App.Instance.TryFindResource("FileWriteErrorMessage");
                Log.Error(errorMessage, ex);
                MessageBox.Show(errorMessage, (string)App.Instance.TryFindResource("ErrorTitle"), MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
