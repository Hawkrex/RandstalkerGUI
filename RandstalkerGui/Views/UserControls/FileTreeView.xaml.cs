using RandstalkerGui.Models.TreeViewElements;
using RandstalkerGui.ViewModels.UserControls;
using System.Windows;
using System.Windows.Controls;

namespace RandstalkerGui.Views.UserControls
{
    /// <summary>
    /// Logique d'interaction pour FileTreeView.xaml
    /// </summary>
    public partial class FileTreeView : UserControl
    {
        public FileTreeView()
        {
            InitializeComponent();
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> eventArgs)
        {
            if(eventArgs.NewValue is TreeViewFile)
            {
                ((FileTreeViewModel)DataContext).SelectedItemChangedHandler(((TreeViewFile)eventArgs.NewValue).Path);
            }
        }
    }
}
