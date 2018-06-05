namespace TrayTimer.ViewModel
{
    using System;
    using TrayTimer.Helpers;

    public class MainViewModel : BaseViewModel
    {
        public ApplicationOptions Options { get; set; }

        public bool WindowVisibility { get; set; } = true;
        public RelayCommand TrayLeftClickCommand { get; set; }
        public RelayCommand SetTimerCommand { get; set; }
        public RelayCommand SetPositionCommand { get; set; }
        public RelayCommand TrayCloseCommand { get; set; }

        public MainViewModel(ApplicationOptions options)
        {
            Options = options;

            SetTimerCommand = new RelayCommand(SetTimer);
            SetPositionCommand = new RelayCommand(SetPosition);
            TrayLeftClickCommand = new RelayCommand(() => { WindowVisibility = true; RaisePropertyChanged(); });
            TrayCloseCommand = new RelayCommand(() => { System.Environment.Exit(1); });
            RaisePropertyChanged();
        }

        private void SetTimer()
        {
            WindowVisibility = false;
            Options.OptionsChanged?.Invoke();
            RaisePropertyChanged();
        }

        private void SetPosition(object option)
        {
            Options.NotificationPosition = (DefaultLocation)Convert.ToInt32(option);
        }
    }
}
