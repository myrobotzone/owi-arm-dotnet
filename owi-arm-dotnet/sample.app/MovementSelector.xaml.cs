using System.Windows;
using System.Windows.Controls;

namespace sample.app;

/// <summary>
/// Interaction logic for MovementSelector.xaml
/// </summary>
public partial class MovementSelector : UserControl
{
    public static DependencyProperty LeftTextProperty =
        DependencyProperty.Register("LeftText", typeof(string), typeof(MovementSelector));

    public static DependencyProperty RightTextProperty =
        DependencyProperty.Register("RightText", typeof(string), typeof(MovementSelector));

    public static DependencyProperty SliderValueProperty =
        DependencyProperty.Register("SliderValue", typeof(int), typeof(MovementSelector));

    public MovementSelector()
    {
        InitializeComponent();
    }

    public string LeftText
    {
        get => (string)GetValue(LeftTextProperty);
        set => SetValue(LeftTextProperty, value);
    }

    public string RightText
    {
        get => (string)GetValue(RightTextProperty);
        set => SetValue(RightTextProperty, value);
    }

    public int SliderValue
    {
        get => (int)GetValue(SliderValueProperty);
        set => SetValue(SliderValueProperty, value);
    }
}