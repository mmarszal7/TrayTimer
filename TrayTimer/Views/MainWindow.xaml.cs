namespace TrayTimer
{
    using System.Windows;
    using TrayTimer.ViewModel;

    public partial class MainWindow : Window
    {
        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
