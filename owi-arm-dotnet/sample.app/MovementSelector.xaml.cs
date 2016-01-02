using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace sample.app
{
    /// <summary>
    /// Interaction logic for MovementSelector.xaml
    /// </summary>
    public partial class MovementSelector : UserControl 
    {
        public MovementSelector()
        {
            InitializeComponent();
        }

        public string LeftText
        {
            get { return (string)GetValue(LeftTextProperty); }
            set { SetValue(LeftTextProperty, value); }
        }



        public static DependencyProperty LeftTextProperty =
           DependencyProperty.Register("LeftText", typeof(string), typeof(MovementSelector));

        public string RightText
        {
            get { return (string)GetValue(RightTextProperty); }
            set { SetValue(RightTextProperty, value); }
        }

        public static DependencyProperty RightTextProperty =
           DependencyProperty.Register("RightText", typeof(string), typeof(MovementSelector));

        public int SliderValue
        {
            get
            {
                return (int)GetValue(SliderValueProperty);
            }
            set
            {
                SetValue(SliderValueProperty, value);
            }
        }

        public static DependencyProperty SliderValueProperty =
           DependencyProperty.Register("SliderValue", typeof(int), typeof(MovementSelector));
    }
}
