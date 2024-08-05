using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.WindowsAPICodePack.Dialogs;
using RandstalkerGui.Models;
using RandstalkerGui.Tools;
using RandstalkerGui.ViewModels.UserControls;
using RandstalkerGui.Views.Popups;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;

namespace RandstalkerGui.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public string CurrentCulture => Thread.CurrentThread.CurrentCulture.Name;

        private readonly Dictionary<object, List<string>> statusBarMessages = new Dictionary<object, List<string>>();

        [RelayCommand]
        public void Close()
        {
            Log.Info($"{nameof(Close)}() => Closing window");

            Application.Current.Shutdown();
        }

        [RelayCommand]
        private void Config()
        {
            var userConfigPopup = new UserConfigPopup();
            userConfigPopup.ShowDialog();
        }

        [RelayCommand]
        private void SwitchLanguage(string languageRegion)
        {
            if (Thread.CurrentThread.CurrentCulture.Name.Equals(languageRegion))
            {
                Log.Info($"{nameof(SwitchLanguage)}() => Language is already {languageRegion}");
            }
            else
            {
                App.Instance.SwitchLanguage(languageRegion);
                OnPropertyChanged(nameof(CurrentCulture));
                Log.Info($"{nameof(SwitchLanguage)}() => Language switched to {languageRegion}");
            }
        }

        [RelayCommand]
        private void About()
        {
            MessageBox.Show((string)App.Instance.TryFindResource("AboutText"), (string)App.Instance.TryFindResource("AboutTitle"), MessageBoxButton.OK);
        }

        public PresetViewModel PresetViewModel { get; set; }

        [ObservableProperty]
        private string statusBarMessage;

        public MainViewModel()
        {
            Log.Info($"--------------------------------------------------");
            Log.Info($"{nameof(MainViewModel)}() => Initialization");

            UserConfig.OnSavedValidUserConfig += SetStatusBarMessage;

            if (string.IsNullOrEmpty(UserConfig.Instance.CheckParametersValidity()))
            {
                PresetViewModel = new PresetViewModel(SetStatusBarMessage);
                return;
            }

            string rootFolderErrorMessage = (string)App.Instance.TryFindResource("UserConfigNotValid") + Environment.NewLine + (string)App.Instance.TryFindResource("SelectRootFolder");
            MessageBox.Show(rootFolderErrorMessage, (string)App.Instance.TryFindResource("UserConfigNotValid"), MessageBoxButton.OK, MessageBoxImage.Error);

            var dialog = new CommonOpenFileDialog
            {
                IsFolderPicker = true
            };

            if (dialog.ShowDialog() != CommonFileDialogResult.Ok)
            {
                return;
            }

            UserConfig.Instance.RandstlakerExeFilePath = Path.Combine(dialog.FileName, "randstalker.exe");
            UserConfig.Instance.PresetsDirectoryPath = Path.Combine(dialog.FileName, "presets");
            UserConfig.Instance.PersonalSettingsDirectoryPath = dialog.FileName;
            UserConfig.Instance.InputRomFilePath = Path.Combine(dialog.FileName, "input.md");
            UserConfig.Instance.OutputRomDirectoryPath = Path.Combine(dialog.FileName, "seeds");

            string parametersInvalid = UserConfig.Instance.CheckParametersValidity();
            if (!string.IsNullOrEmpty(parametersInvalid))
            {
                MessageBox.Show(parametersInvalid, (string)App.Instance.TryFindResource("UserConfigNotValid"), MessageBoxButton.OK, MessageBoxImage.Warning);

                SetStatusBarMessage(this, new StatusBarMessageEventArgs() { Message = parametersInvalid, Sender = "UserConfig" });
            }
            else
            {
                try
                {
                    UserConfig.SaveFile();
                }
                catch (Exception ex)
                {
                    string fileErrorMessage = (string)App.Instance.TryFindResource("FileWriteErrorMessage");
                    Log.Error(fileErrorMessage, ex);
                    MessageBox.Show(fileErrorMessage, (string)App.Instance.TryFindResource("ErrorTitle"), MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            PresetViewModel = new PresetViewModel(SetStatusBarMessage);
        }

        public void SetStatusBarMessage(object sender, StatusBarMessageEventArgs args)
        {
            if (statusBarMessages.ContainsKey(args.Sender))
            {
                if (string.IsNullOrEmpty(args.Message))
                {
                    statusBarMessages.Remove(args.Sender);
                }
                else
                {
                    statusBarMessages[args.Sender].Add(args.Message);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(args.Message))
                {
                    statusBarMessages.Add(args.Sender, new List<string>() { args.Message });
                }
            }

            StatusBarMessage = statusBarMessages?.FirstOrDefault().Value?.FirstOrDefault();
        }
    }
}
