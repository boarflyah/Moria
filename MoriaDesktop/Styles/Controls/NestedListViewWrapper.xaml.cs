using System.Windows;
using System.Windows.Controls;

namespace MoriaDesktop.Styles.Controls;
public partial class NestedListViewWrapper : UserControl
{
    public NestedListViewWrapper()
    {
        InitializeComponent();

    }

    //public static readonly DependencyProperty LabelTitleProperty =
    //    DependencyProperty.Register(
    //        nameof(LabelTitle),
    //        typeof(string),
    //        typeof(NestedListViewWrapper),
    //        new PropertyMetadata(""));

    //public string LabelTitle
    //{
    //    get => (string)GetValue(LabelTitleProperty);
    //    set => SetValue(LabelTitleProperty, value);
    //}
}
