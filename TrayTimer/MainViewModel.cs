using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Core;
using System;
using System.Windows;
using System.Windows.Threading;

namespace TrayTimer
{
    [POCOViewModel]
    public class MainViewModel : ViewModelBase
    {
        #region Properties 

        public virtual double TimeInterval { get; set; } = 1;
        public virtual string NotificationText { get; set; } = "Reminder";
        public virtual string WindowVisability { get; set; } = "Normal";
        private DispatcherTimer timer;
        Notification notificationWindow;

        #endregion

        #region DevExpress Constructor

        public static MainViewModel Create()
        {
            return ViewModelSource.Create(() => new MainViewModel());
        }
        protected MainViewModel()
        {
            if (this.IsInDesignMode()) return;
        }

        #endregion

        #region Commands 

        public void TrayLeftClick() { WindowVisability = "Normal"; }

        public void SetTimer()
        {
            WindowVisability = "Minimized";
            timer = new DispatcherTimer(TimeSpan.FromSeconds(TimeInterval*60), DispatcherPriority.Send, OnTimerTick, Dispatcher.CurrentDispatcher);
            timer.Start();
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            if(notificationWindow != null) notificationWindow.Close();
            notificationWindow = new Notification(NotificationText);
            notificationWindow.Show();
        }

        #endregion
    }
}
