using System.Windows;
using System.Windows.Controls;

namespace RandstalkerGui.Views.UserControls.SubPresets
{
    /// <summary>
    /// Interaction logic for ItemsListView.xaml
    /// </summary>
    public partial class ItemsListView : UserControl
    {
        public static readonly DependencyProperty GroupBoxHeaderProperty =
        DependencyProperty.Register(
            "GroupBoxHeader",
            typeof(string),
            typeof(ItemsListView),
            new PropertyMetadata(string.Empty));

        public string GroupBoxHeader
        {
            get { return (string)GetValue(GroupBoxHeaderProperty); }
            set { SetValue(GroupBoxHeaderProperty, value); }
        }

        public ItemsListView()
        {
            InitializeComponent();
        }
    }
}
