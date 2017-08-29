﻿using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Threading;

namespace TrayTimer
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Properties 

        public double timeInterval = 1;
        public string notificationText = "Reminder";
        public string windowVisability = "Normal";
        private DispatcherTimer timer;
        Notification notificationWindow;
        private int clicks = 0;
        private int allTicks = 0;

        public string ToolTipText { get { return "Double click to close. \n Number of clicks: " + clicks.ToString() + "/" + allTicks.ToString(); } }

        public double TimeInterval
        {
            get { return timeInterval; }
            set
            {
                timeInterval = value;
                RaisePropertyChanged();
            }
        }

        public string NotificationText
        {
            get { return notificationText; }
            set
            {
                notificationText = value;
            }
        }

        public string WindowVisability
        {
            get { return windowVisability; }
            set
            {
                windowVisability = value;
            }
        }

        public int Clicks
        {
            get { return clicks; }
            set
            {
                clicks = value;
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
            RaisePropertyChanged();
            timer = new DispatcherTimer(TimeSpan.FromSeconds(TimeInterval * 60), DispatcherPriority.Send, OnTimerTick, Dispatcher.CurrentDispatcher);
            timer.Start();
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            if (notificationWindow != null)
            {
                Clicks += notificationWindow.NotificationClicks;
                notificationWindow.Close();
            }
            notificationWindow = new Notification(NotificationText);
            notificationWindow.Show();
            allTicks++;
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
