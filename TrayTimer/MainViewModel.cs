using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Threading;

namespace TrayTimer
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Properties 

        public double TimeInterval { get; set; } = 1;
        public string NotificationText { get; set; } = "Reminder";
        public string windowVisability = "Normal";
        private DispatcherTimer timer;
        Notification notificationWindow;

        public string WindowVisability
        {
            get { return windowVisability; }
            set
            {
                windowVisability = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Constructor and Commands

        public MainViewModel()
        {
            TrayLeftClickCommand = new RelayCommand(() => { WindowVisability = "Normal"; });
            SetTimerCommand = new RelayCommand(SetTimer);
            TrayCloseCommand = new RelayCommand(() => { System.Environment.Exit(1); });
        }

        public RelayCommand TrayLeftClickCommand { get; set; }
        public RelayCommand SetTimerCommand { get; set; }
        public RelayCommand TrayCloseCommand { get; set; }

        public void SetTimer()
        {
            WindowVisability = "Minimized";
            timer = new DispatcherTimer(TimeSpan.FromSeconds(TimeInterval * 60), DispatcherPriority.Send, OnTimerTick, Dispatcher.CurrentDispatcher);
            timer.Start();
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            if (notificationWindow != null) notificationWindow.Close();
            notificationWindow = new Notification(NotificationText);
            notificationWindow.Show();
        }

        #endregion

        #region PropertyChanged 

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged()
        {
            if (PropertyChanged != null)
            {
                foreach (var property in this.GetType().GetProperties().Where(p => !p.IsSpecialName))
                    PropertyChanged(this, new PropertyChangedEventArgs(property.Name));
            }
        }

        #endregion
    }
}
