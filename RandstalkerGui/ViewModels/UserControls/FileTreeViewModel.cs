using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RandstalkerGui.Models.TreeViewElements;
using RandstalkerGui.ViewModels.Popups;
using RandstalkerGui.Views.Popups;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;

namespace RandstalkerGui.ViewModels.UserControls
{
    public class FileTreeViewModel : ObservableObject
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly string basePath = string.Empty;
        private readonly IDictionary<string, string> acceptedExtensions;
        private readonly byte[] defaultFile;

        public ObservableCollection<TreeViewElement> Tree { get; set; }

        private bool contextMenuEnabled;
        public bool ContextMenuEnabled
        {
            get => contextMenuEnabled;
            set => SetProperty(ref contextMenuEnabled, value);
        }

        private string selectedFileRelativePath;
        public string SelectedFileRelativePath
        {
            get => selectedFileRelativePath;
            set => SetProperty(ref selectedFileRelativePath, value);
        }

        public RelayCommand<string> NewDirectory => new(NewDirectoryHandler);
        private void NewDirectoryHandler(string directoryPath)
        {
            string newDirectoryPath = string.Empty;

            try
            {
                var win = new InputDialog();
                if (win.ShowDialog().Value)
                {
                    newDirectoryPath = Path.Combine(directoryPath, ((InputDialogViewModel)win.DataContext).Input);
                    Directory.CreateDirectory(newDirectoryPath);

                    UpdateTree();
                }
            }
            catch (Exception ex)
            {
                Log.Error($"{nameof(NewDirectoryHandler)}() => Impossible to create the directory <{newDirectoryPath}>", ex);
            }
        }

        public RelayCommand<string> NewFile => new(NewFileHandler);
        private void NewFileHandler(string directoryPath)
        {
            var saveFileDialog = new SaveFileDialog();

            try
            {
                string filter = string.Empty;
                foreach (var extension in acceptedExtensions)
                {
                    filter += extension.Value + "|";
                }
                filter = filter.TrimEnd('|');
                saveFileDialog.Filter = filter;

                saveFileDialog.InitialDirectory = directoryPath;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Stream savingStream;
                    if ((savingStream = saveFileDialog.OpenFile()) != null)
                    {
                        savingStream.Write(defaultFile, 0, defaultFile.Length);
                        savingStream.Close();
                    }
                }

                UpdateTree();
            }
            catch (Exception ex)
            {
                Log.Error($"{nameof(NewFileHandler)}() => Impossible to create the file <{saveFileDialog.FileName}>", ex);
            }
        }

        public RelayCommand<string> DuplicateFile => new(DuplicateFileHandler);
        private void DuplicateFileHandler(string filePath)
        {
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
                Log.Error($"{nameof(DuplicateFileHandler)}() => Impossible to duplicate the file <{filePath}>", ex);
            }
        }

        public RelayCommand<string> DeleteDirectory => new(DeleteDirectoryHandler);
        private void DeleteDirectoryHandler(string directoryPath)
        {
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
                Log.Error($"{nameof(DeleteDirectoryHandler)}() => Impossible to delete the directory <{directoryPath}>", ex);
            }
        }

        public RelayCommand<string> DeleteFile => new(DeleteFileHandler);
        private void DeleteFileHandler(string filePath)
        {
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
                Log.Error($"{nameof(DeleteFileHandler)}() => Impossible to delete the file <{SelectedFileRelativePath}>", ex);
            }
        }

        public FileTreeViewModel(string basePath, IDictionary<string, string> acceptedExtensions, byte[] defaultFile, string defaultSelectedFilePath, bool canExecuteCommands = true)
        {
            this.basePath = basePath;
            this.acceptedExtensions = acceptedExtensions;
            this.defaultFile = defaultFile;
            SelectedFileRelativePath = defaultSelectedFilePath;

            ContextMenuEnabled = canExecuteCommands;

            Tree = new ObservableCollection<TreeViewElement>();
            UpdateTree();
        }

        public void SelectedItemChangedHandler(string selectedItem)
        {
            SelectedFileRelativePath = selectedItem.Substring(basePath.Length + 1);
        }

        /// <summary>
        /// Clear and recreate file tree
        /// </summary>
        private void UpdateTree()
        {
            Tree.Clear();
            if (string.IsNullOrEmpty(basePath))
            {
                return;
            }

            var baseDirInfo = new DirectoryInfo(basePath);
            foreach (var item in GetItems(baseDirInfo.FullName))
            {
                Tree.Add(item);
            }
        }

        /// <summary>
        /// Get all items in a folder recursively
        /// </summary>
        /// <param name="path">Full path of the folder to get items from</param>
        /// <returns>A collection of treeview elements (File or directory)</returns>
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

            foreach (var file in dirInfo.GetFiles().Where(f => acceptedExtensions.ContainsKey(f.Extension)))
            {
                var item = new TreeViewFile
                {
                    Name = Path.GetFileNameWithoutExtension(file.Name),
                    Path = file.FullName
                };

                items.Add(item);
            }

            return items;
        }
    }
}
