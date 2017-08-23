using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Core;
using System;
using System.Windows.Threading;

namespace TrayTimer
{
    #region NotificationViewModel 

    [POCOViewModel]
    public class NotificationViewModel
    {
        public virtual string Text { get; set; }
        public virtual int Time { get; set; }
    }

    #endregion

    [POCOViewModel]
    public class MainViewModel : ViewModelBase
    {
        #region Properties 

        public virtual int TimeInterval { get; set; } = 5;
        public virtual string NotificationText { get; set; } = "Sample";

        public virtual string WindowVisability { get; set; } = "Normal";
        public virtual INotificationService CustomNotificationService { get { return null; } }
        private NotificationViewModel notificationViewModel;
        private INotification notification;
        private DispatcherTimer timer;

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
            //TimeInterval*60
            WindowVisability = "Minimized";
            timer = new DispatcherTimer(TimeSpan.FromSeconds(TimeInterval), DispatcherPriority.Send, OnTimerTick, Dispatcher.CurrentDispatcher);
            timer.Start();
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            notificationViewModel = ViewModelSource.Create(() => new NotificationViewModel() { Text = NotificationText, Time = TimeInterval });
            notification = CustomNotificationService.CreateCustomNotification(notificationViewModel);
            notification.ShowAsync();
        }

        #endregion
    }
}
