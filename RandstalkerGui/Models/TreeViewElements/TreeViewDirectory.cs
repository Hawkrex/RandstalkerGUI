using System.Collections.ObjectModel;

namespace RandstalkerGui.Models.TreeViewElements
{
    public class TreeViewDirectory : TreeViewElement
    {
        public ObservableCollection<TreeViewElement> Items { get; set; }

        public TreeViewDirectory()
        {
            Items = new ObservableCollection<TreeViewElement>();
        }
    }
}
