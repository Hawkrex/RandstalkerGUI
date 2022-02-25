using System.Windows;
using System.Windows.Controls;

namespace RandstalkerGui.Views.UserControls.SubPresets
{
    /// <summary>
    /// Logique d'interaction pour ItemsCounterView.xaml
    /// </summary>
    public partial class ItemsCounterView : UserControl
    {
        public static readonly DependencyProperty GroupBoxHeaderProperty =
        DependencyProperty.Register(
            "GroupBoxHeader",
            typeof(string),
            typeof(ItemsCounterView),
            new PropertyMetadata(string.Empty));

        public string GroupBoxHeader
        {
            get { return (string)GetValue(GroupBoxHeaderProperty); }
            set { SetValue(GroupBoxHeaderProperty, value); }
        }

        public ItemsCounterView()
        {
            InitializeComponent();
        }
    }
}
