using System.ComponentModel;
using System.Windows;
using sample.app.ViewModel;

namespace sample.app;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        Closing += MainWindow_Closing;
    }

    private void MainWindow_Closing(object sender, CancelEventArgs e)
    {
        ViewModelLocator.Cleanup();
        Closing -= MainWindow_Closing;
    }
}