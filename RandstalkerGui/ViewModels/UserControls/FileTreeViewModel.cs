using RandstalkerGui.Models.TreeViewElements;
using RandstalkerGui.Tools;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace RandstalkerGui.ViewModels.UserControls
{
    public class FileTreeViewModel : BaseViewModel
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private string _basePath;

        public ObservableCollection<TreeViewElement> Tree { get; set; }

        private string _selectedFileRelativePath;
        public string SelectedFileRelativePath
        {
            get
            {
                return _selectedFileRelativePath;
            }
            set
            {
                if (_selectedFileRelativePath != value)
                {
                    Log.Debug($"{nameof(SelectedFileRelativePath)} => <{_selectedFileRelativePath}> will change to <{value}>");
                    _selectedFileRelativePath = value;
                    OnPropertyChanged();
                }
            }
        }

        public RelayCommand NewDirectory { get { return new RelayCommand(_ => NewDirectoryHandler()); } }
        private void NewDirectoryHandler()
        {
            Log.Debug($"{nameof(NewDirectoryHandler)}() => Command requested ...");


            Log.Debug($"{nameof(NewDirectoryHandler)}() => Command executed");
        }

        public RelayCommand DuplicateDirectory { get { return new RelayCommand(_ => DuplicateDirectoryHandler()); } }
        private void DuplicateDirectoryHandler()
        {
            Log.Debug($"{nameof(DuplicateDirectoryHandler)}() => Command requested ...");


            Log.Debug($"{nameof(DuplicateDirectoryHandler)}() => Command executed");
        }

        public RelayCommand DeleteDirectory { get { return new RelayCommand(_ => DeleteDirectoryHandler()); } }
        private void DeleteDirectoryHandler()
        {
            Log.Debug($"{nameof(DeleteDirectoryHandler)}() => Command requested ...");

            try
            {
                if (MessageBox.Show("Delete Directory ?", "DeleteDirectory", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Directory.Delete(_basePath + SelectedFileRelativePath);
                }
            }
            catch(Exception ex)
            {

            }

            Log.Debug($"{nameof(DeleteDirectoryHandler)}() => Command executed");
        }

        public RelayCommand NewFile { get { return new RelayCommand(_ => NewFileHandler()); } }
        private void NewFileHandler()
        {
            Log.Debug($"{nameof(NewFileHandler)}() => Command requested ...");


            Log.Debug($"{nameof(NewFileHandler)}() => Command executed");
        }

        public RelayCommand DuplicateFile { get { return new RelayCommand(_ => DuplicateFileHandler()); } }
        private void DuplicateFileHandler()
        {
            Log.Debug($"{nameof(DuplicateFileHandler)}() => Command requested ...");

            try
            {
                if (MessageBox.Show("Delete file ?", "DeleteFile", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    File.Copy(_basePath + SelectedFileRelativePath, _basePath + SelectedFileRelativePath + "_Copy");
                }
            }
            catch (Exception ex)
            {

            }

            Log.Debug($"{nameof(DuplicateFileHandler)}() => Command executed");
        }

        public RelayCommand DeleteFile { get { return new RelayCommand(_ => DeleteFileHandler()); } }
        private void DeleteFileHandler()
        {
            Log.Debug($"{nameof(DeleteFileHandler)}() => Command requested ...");

            try
            {
                if(MessageBox.Show("Delete file ?", "DeleteFile", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    File.Delete(_basePath + SelectedFileRelativePath);
                }
            }
            catch (Exception ex)
            {

            }

            Log.Debug($"{nameof(DeleteFileHandler)}() => Command executed");
        }

        public FileTreeViewModel(string basepath)
        {
            _basePath = basepath;

            var baseDirInfo = new DirectoryInfo(basepath);
            Tree = new ObservableCollection<TreeViewElement>()
            {
                new TreeViewDirectory
                {
                    Name = baseDirInfo.Name,
                    Path = baseDirInfo.FullName,
                    Items = GetItems(baseDirInfo.FullName)
                }
            };

            SelectedFileRelativePath = "default.json";
        }

        public void SelectedItemChangedHandler(string selectedItem)
        {
            Log.Debug($"{nameof(SelectedItemChangedHandler)}() => Command requested ...");

            SelectedFileRelativePath = selectedItem.Substring(_basePath.Length + 1);

            Log.Debug($"{nameof(SelectedItemChangedHandler)}() => Command executed");
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
