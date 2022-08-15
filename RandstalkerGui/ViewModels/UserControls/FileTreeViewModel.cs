using RandstalkerGui.Models.TreeViewElements;
using RandstalkerGui.Tools;
using RandstalkerGui.ViewModels.Popups;
using RandstalkerGui.Views.Popups;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;

namespace RandstalkerGui.ViewModels.UserControls
{
    public class FileTreeViewModel : BaseViewModel
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private string basePath = string.Empty;

        private byte[] defaultFile;

        public ObservableCollection<TreeViewElement> Tree { get; set; }

        private bool contextMenuEnabled;
        public bool ContextMenuEnabled
        {
            get
            {
                return contextMenuEnabled;
            }
            set
            {
                if (contextMenuEnabled != value)
                {
                    Log.Debug($"{nameof(ContextMenuEnabled)} => <{contextMenuEnabled}> will change to <{value}>");
                    contextMenuEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        private string selectedFileRelativePath;
        public string SelectedFileRelativePath
        {
            get
            {
                return selectedFileRelativePath;
            }
            set
            {
                if (selectedFileRelativePath != value)
                {
                    Log.Debug($"{nameof(SelectedFileRelativePath)} => <{selectedFileRelativePath}> will change to <{value}>");
                    selectedFileRelativePath = value;
                    OnPropertyChanged();
                }
            }
        }

        public RelayCommand<string> NewDirectory { get { return new RelayCommand<string>(param => NewDirectoryHandler(param)); } }
        private void NewDirectoryHandler(string directoryPath)
        {
            Log.Debug($"{nameof(NewDirectoryHandler)}() => Command requested ...");

            string newDirectoryPath = string.Empty;

            try
            {
                InputDialog win = new InputDialog();
                if (win.ShowDialog().Value)
                {
                    newDirectoryPath = Path.Combine(directoryPath, ((InputDialogViewModel)win.DataContext).Input);
                    Directory.CreateDirectory(newDirectoryPath);

                    UpdateTree();
                }
            }
            catch (Exception ex)
            {
                Log.Error($"{nameof(NewDirectoryHandler)}() => Impossible to create the directory <{newDirectoryPath}> : {ex}");
            }

            Log.Debug($"{nameof(NewDirectoryHandler)}() => Command executed");
        }

        public RelayCommand<string> NewFile { get { return new RelayCommand<string>(param => NewFileHandler(param)); } }
        private void NewFileHandler(string directoryPath)
        {
            Log.Debug($"{nameof(NewFileHandler)}() => Command requested ...");

            string newFilePath = string.Empty;

            try
            {
                Stream savingStream;
                var saveFileDialog = new SaveFileDialog();

                saveFileDialog.Filter = "json files (*.json)|*.json";
                saveFileDialog.InitialDirectory = directoryPath;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if ((savingStream = saveFileDialog.OpenFile()) != null)
                    {
                        newFilePath = saveFileDialog.FileName;
                        savingStream.Write(defaultFile, 0, defaultFile.Length);
                        savingStream.Close();
                    }
                }

                UpdateTree();
            }
            catch (Exception ex)
            {
                Log.Error($"{nameof(NewFileHandler)}() => Impossible to create the file <{newFilePath}> : {ex}");
            }

            Log.Debug($"{nameof(NewFileHandler)}() => Command executed");
        }

        public RelayCommand<string> DuplicateFile { get { return new RelayCommand<string>(param => DuplicateFileHandler(param)); } }
        private void DuplicateFileHandler(string filePath)
        {
            Log.Debug($"{nameof(DuplicateFileHandler)}() => Command requested ...");

            try
            {
                if (MessageBox.Show((string)App.Instance.TryFindResource("DuplicateFileAskTitle"), (string)App.Instance.TryFindResource("DuplicateFileAskMessage"), MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    File.Copy(filePath, Path.Combine(Path.GetDirectoryName(filePath), Path.GetFileNameWithoutExtension(filePath) + "Copy" + Path.GetExtension(filePath)));

                    UpdateTree();
                }
            }
            catch (Exception ex)
            {
                Log.Error($"{nameof(DuplicateFileHandler)}() => Impossible to duplicate the file <{filePath}> : {ex}");
            }

            Log.Debug($"{nameof(DuplicateFileHandler)}() => Command executed");
        }

        public RelayCommand<string> DeleteDirectory { get { return new RelayCommand<string>(param => DeleteDirectoryHandler(param)); } }
        private void DeleteDirectoryHandler(string directoryPath)
        {
            Log.Debug($"{nameof(DeleteDirectoryHandler)}() => Command requested ...");

            try
            {
                if (MessageBox.Show((string)App.Instance.TryFindResource("DeleteDirectoryAskMessage"), (string)App.Instance.TryFindResource("DeleteDirectoryAskTitle"), MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Directory.Delete(directoryPath, true);

                    UpdateTree();
                }
            }
            catch (Exception ex)
            {
                Log.Error($"{nameof(DeleteDirectoryHandler)}() => Impossible to delete the directory <{directoryPath}> : {ex}");
            }

            Log.Debug($"{nameof(DeleteDirectoryHandler)}() => Command executed");
        }

        public RelayCommand<string> DeleteFile { get { return new RelayCommand<string>(param => DeleteFileHandler(param)); } }
        private void DeleteFileHandler(string filePath)
        {
            Log.Debug($"{nameof(DeleteFileHandler)}() => Command requested ...");

            try
            {
                if (MessageBox.Show((string)App.Instance.TryFindResource("DeleteFileAskMessage"), (string)App.Instance.TryFindResource("DeleteFileAskTitle"), MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    File.Delete(filePath);

                    UpdateTree();
                }
            }
            catch (Exception ex)
            {
                Log.Error($"{nameof(DeleteFileHandler)}() => Impossible to delete the file <{SelectedFileRelativePath}> : {ex}");
            }

            Log.Debug($"{nameof(DeleteFileHandler)}() => Command executed");
        }

        public FileTreeViewModel(string basePath, string defaultSelectedFilePath, byte[] defaultFile, bool canExecuteCommands = true)
        {
            this.basePath = basePath;
            SelectedFileRelativePath = defaultSelectedFilePath;
            this.defaultFile = defaultFile;
            ContextMenuEnabled = canExecuteCommands;

            Tree = new ObservableCollection<TreeViewElement>();
            UpdateTree();
        }

        public void SelectedItemChangedHandler(string selectedItem)
        {
            Log.Debug($"{nameof(SelectedItemChangedHandler)}() => Command requested ...");

            SelectedFileRelativePath = selectedItem.Substring(basePath.Length + 1);

            Log.Debug($"{nameof(SelectedItemChangedHandler)}() => Command executed");
        }

        private void UpdateTree()
        {
            Tree.Clear();
            if (string.IsNullOrEmpty(basePath))
            {
                return;
            }

            var baseDirInfo = new DirectoryInfo(basePath);
            Tree.Add(new TreeViewDirectory
            {
                Name = baseDirInfo.Name,
                Path = baseDirInfo.FullName,
                Items = GetItems(baseDirInfo.FullName)
            });
        }

        private ObservableCollection<TreeViewElement> GetItems(string path)
        {
            var items = new ObservableCollection<TreeViewElement>();

            var dirInfo = new DirectoryInfo(path);

            foreach (var directory in dirInfo.GetDirectories())
            {
                var item = new TreeViewDirectory
                {
                    Name = directory.Name,
                    Path = directory.FullName,
                    Items = GetItems(directory.FullName)
                };

                items.Add(item);
            }

            foreach (var file in dirInfo.GetFiles())
            {
                var item = new TreeViewFile
                {
                    Name = file.Name,
                    Path = file.FullName
                };

                items.Add(item);
            }

            return items;
        }
    }
}
