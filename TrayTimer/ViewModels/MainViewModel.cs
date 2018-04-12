namespace TrayTimer.ViewModel
{
    using TrayTimer.Helpers;

    public class MainViewModel : BaseViewModel
    {
        private Notification NotificationWindow;
        public double NotificationTimeInterval { get; set; } = 1;
        public string NotificationText { get; set; } = "Reminder";
        public bool WindowVisibility { get; set; } = true;
        public RelayCommand TrayLeftClickCommand { get; set; }
        public RelayCommand SetTimerCommand { get; set; }
        public RelayCommand TrayCloseCommand { get; set; }

        public MainViewModel(Notification notification)
        {
            NotificationWindow = notification;

            SetTimerCommand = new RelayCommand(SetTimer);
            TrayLeftClickCommand = new RelayCommand(() => { WindowVisibility = true; RaisePropertyChanged(); });
            TrayCloseCommand = new RelayCommand(() => { System.Environment.Exit(1); });
        }

        public void SetTimer()
        {
            WindowVisibility = false;
            NotificationWindow.SetNotifiction(NotificationText, NotificationTimeInterval);
            RaisePropertyChanged();
        }
    }
}
