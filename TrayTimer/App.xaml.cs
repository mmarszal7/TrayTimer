namespace TrayTimer
{
    using System.ComponentModel;
    using System.Windows;
    using TrayTimer.ViewModel;

    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Application.Current.MainWindow = new MainWindow(new MainViewModel(new Notification()));
            Application.Current.MainWindow.Closing += (object sender, CancelEventArgs ev) => System.Environment.Exit(1);
            Application.Current.MainWindow.Show();
        }
    }
}
