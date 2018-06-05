namespace TrayTimer
{
    using System.ComponentModel;
    using System.Windows;
    using TrayTimer.Helpers;
    using TrayTimer.ViewModel;

    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var options = new ApplicationOptions();
            var notifications = new Notification(options);
            var viewModel = new MainViewModel(options);

            Application.Current.MainWindow = new MainWindow(viewModel);
            Application.Current.MainWindow.Closing += (object sender, CancelEventArgs ev) => System.Environment.Exit(1);
            Application.Current.MainWindow.Show();
        }
    }
}
