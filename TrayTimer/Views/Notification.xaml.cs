using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using TrayTimer.Helpers;

namespace TrayTimer
{
    public partial class Notification : Window
    {
        private ApplicationOptions Options;
        private Random rnd = new Random();
        private DispatcherTimer timer;
        private int allTicks = 0;
        private int clicks = 0;

        public Notification(ApplicationOptions options)
        {
            Options = options;
            Options.OptionsChanged += SetNotifiction;
            InitializeComponent();
        }

        public void SetNotifiction()
        {
            NotificationTitleTextBox.Text = Options.NotificationText;
            allTicks = 0;
            clicks = 0;

            timer?.Stop();
            timer = new DispatcherTimer(TimeSpan.FromSeconds(Options.IntervalTime * 60), DispatcherPriority.Send, OnTimerTick, Dispatcher.CurrentDispatcher);
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            allTicks++;
            SetPosition();
            SetColor();
            NotificationBody.ToolTip = $"Clicks: {clicks}/{allTicks}";

            this.Show();
        }

        private void SetColor()
        {
            if (Options.IsColorRandom)
            {
                NotificationBackground.Background = Options.ColorList[rnd.Next(0, 4)];
            }
            else
            {
                NotificationBackground.Background = new SolidColorBrush(Colors.DarkGray);
            }
        }

        private void SetPosition()
        {
            int marginX = 150;
            int marginY = 20;
            var workingArea = System.Windows.SystemParameters.WorkArea;

            switch (Options.NotificationPosition)
            {
                case DefaultLocation.Random:
                    this.Left = rnd.Next(marginX, (int)workingArea.Width - marginX - (int)NotificationBody.Width);
                    this.Top = rnd.Next(marginY, (int)workingArea.Height - marginY - (int)NotificationBody.Height);
                    break;
                case DefaultLocation.UpperLeft:
                    this.Left = marginX / 2 - (int)NotificationBody.Width;
                    this.Top = 0;
                    break;
                case DefaultLocation.UpperRight:
                    this.Left = (int)workingArea.Width - marginX / 2 - (int)NotificationBody.Width;
                    this.Top = 0;
                    break;
                case DefaultLocation.LowerLeft:
                    this.Left = marginX / 2 - (int)NotificationBody.Width;
                    this.Top = (int)workingArea.Height - (int)NotificationBody.Height;
                    break;
                case DefaultLocation.LowerRight:
                default:
                    this.Left = (int)workingArea.Width - marginX / 2 - (int)NotificationBody.Width;
                    this.Top = (int)workingArea.Height - (int)NotificationBody.Height;
                    break;
            }
        }

        private void NotificationClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            clicks += 1;
            this.Visibility = Visibility.Hidden;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
