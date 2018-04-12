using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace TrayTimer
{
    public partial class Notification : Window
    {
        public bool IsColorRandom { get; set; }
        public bool IsPositionRandom { get; set; }
        private Random rnd = new Random();
        private int allTicks = 0;
        private int clicks = 0;
        private DispatcherTimer timer;
        private readonly SolidColorBrush[] ColorList = new SolidColorBrush[5]
        {
            new SolidColorBrush(Colors.DarkRed),
            new SolidColorBrush(Colors.DarkBlue),
            new SolidColorBrush(Colors.DarkGreen),
            new SolidColorBrush(Colors.DarkOrange),
            new SolidColorBrush(Colors.DarkViolet)
        };

        public Notification()
        {
            InitializeComponent();
        }

        public void SetNotifiction(string title, double timeInterval)
        {
            NotificationTitleTextBox.Text = title;
            allTicks = 0;
            clicks = 0;

            timer = new DispatcherTimer(TimeSpan.FromSeconds(timeInterval * 60), DispatcherPriority.Send, OnTimerTick, Dispatcher.CurrentDispatcher);
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
            if (IsColorRandom)
            {
                NotificationBackground.Background = ColorList[rnd.Next(0, 4)];
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
            
            if (IsPositionRandom)
            {
                this.Left = rnd.Next(marginX, (int)workingArea.Width - marginX - (int)NotificationBody.Width);
                this.Top = rnd.Next(marginY, (int)workingArea.Height - marginY - (int)NotificationBody.Height);
            }
            else
            {
                this.Left = (int)workingArea.Width - marginX/2 - (int)NotificationBody.Width;
                this.Top = (int)workingArea.Height - (int)NotificationBody.Height;
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
