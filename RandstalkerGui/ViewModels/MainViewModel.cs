using Microsoft.WindowsAPICodePack.Dialogs;
using Newtonsoft.Json;
using RandstalkerGui.Models;
using RandstalkerGui.Tools;
using RandstalkerGui.ValidationRules;
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
    public class MainViewModel : BaseViewModel
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private Dictionary<object, List<string>> statusBarMessages = new Dictionary<object, List<string>>();

        public RelayCommand OnClose { get { return new RelayCommand(_ => OnCloseHandler()); } }

        public void OnCloseHandler()
        {
            Log.Info($"{nameof(OnCloseHandler)}() => Closing window");

            Application.Current.Shutdown();
        }

        public RelayCommand Config { get { return new RelayCommand(_ => ConfigHandler()); } }

        private void ConfigHandler()
        {
            Log.Debug($"{nameof(ConfigHandler)}() => Command requested ...");

            var userConfigPopup = new UserConfigPopup();
            userConfigPopup.ShowDialog();

            Log.Debug($"{nameof(ConfigHandler)}() => Command executed");
        }

        public string CurrentCulture
        {
            get
            {
                return Thread.CurrentThread.CurrentCulture.Name;
            }
        }

        public RelayCommand SwitchLanguage { get { return new RelayCommand(param => SwitchLanguageHandler(param)); } }
        private void SwitchLanguageHandler(object param)
        {
            Log.Debug($"{nameof(SwitchLanguageHandler)}() => Command requested ...");

            string languageRegion = param.ToString();
            if (languageRegion == null)
            {
                throw new ArgumentException("Parameter is not a string");
            }

            if (Thread.CurrentThread.CurrentCulture.Name.Equals(languageRegion))
            {
                Log.Info($"{nameof(SwitchLanguageHandler)}() => Language is already {languageRegion}");
            }
            else
            {
                App.Instance.SwitchLanguage(languageRegion);
                OnPropertyChanged(nameof(CurrentCulture));
                Log.Info($"{nameof(SwitchLanguageHandler)}() => Language switched to {languageRegion}");
            }

            Log.Debug($"{nameof(SwitchLanguageHandler)}() => Command executed");
        }

        public RelayCommand About { get { return new RelayCommand(_ => AboutHandler()); } }

        private void AboutHandler()
        {
            Log.Debug($"{nameof(AboutHandler)}() => Command requested ...");

            MessageBox.Show((string)App.Instance.TryFindResource("AboutText"), (string)App.Instance.TryFindResource("AboutTitle"), MessageBoxButton.OK);

            Log.Debug($"{nameof(AboutHandler)}() => Command executed");
        }

        public PresetViewModel PresetViewModel { get; set; }

        private string statusBarMessage;
        public string StatusBarMessage
        {
            get
            {
                return statusBarMessage;
            }
            set
            {
                if (statusBarMessage != value)
                {
                    Log.Debug($"{nameof(StatusBarMessage)} => <{statusBarMessage}> will change to <{value}>");
                    statusBarMessage = value;
                    OnPropertyChanged();
                }
            }
        }

        public MainViewModel()
        {
            Log.Info($"--------------------------------------------------");
            Log.Info($"{nameof(MainViewModel)}() => Initialization");

            UserConfig.OnSavedValidUserConfig += SetStatusBarMessage;

            PresetViewModel = new PresetViewModel(SetStatusBarMessage);
            PresetViewModel.OnError += SetStatusBarMessage;

            if (string.IsNullOrEmpty(UserConfig.Instance.CheckParametersValidity()))
            {
                return;
            }

            string rootFolderErrorMessage = (string)App.Instance.TryFindResource("UserConfigNotValid") + Environment.NewLine + (string)App.Instance.TryFindResource("SelectRootFolder");
            MessageBox.Show(rootFolderErrorMessage, (string)App.Instance.TryFindResource("UserConfigNotValid"), MessageBoxButton.OK, MessageBoxImage.Error);

            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;

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
                    File.WriteAllText("Resources/userConfig.json", JsonConvert.SerializeObject(UserConfig.Instance));
                }
                catch (Exception ex)
                {
                    string fileErrorMessage = (string)App.Instance.TryFindResource("FileWriteErrorMessage");
                    MessageBox.Show(fileErrorMessage, (string)App.Instance.TryFindResource("ErrorTitle"), MessageBoxButton.OK, MessageBoxImage.Error);
                    Log.Error(fileErrorMessage + " : " + ex);
                }
            }
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
