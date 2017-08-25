using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace TrayTimer
{
    public partial class Notification : Window
    {
        public int NotificationClicks = 0;
        private void NotificationClick(object sender, System.Windows.Input.MouseButtonEventArgs e) { NotificationClicks = 1 ; }
        private int marginX = 150;
        private int marginY = 20;

        private Random rnd = new Random();
        private SolidColorBrush[] ColorList = new SolidColorBrush[5]
        {
            new SolidColorBrush(Colors.DarkRed),
            new SolidColorBrush(Colors.DarkBlue),
            new SolidColorBrush(Colors.DarkGreen),
            new SolidColorBrush(Colors.DarkOrange),
            new SolidColorBrush(Colors.DarkViolet)
        };

        public Notification(string title)
        {
            InitializeComponent();

            Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, new Action(() =>
            {
                var workingArea = System.Windows.SystemParameters.WorkArea;
                
                this.Left = rnd.Next(marginX, (int)workingArea.Width - marginX - (int)NotificationBody.Width);
                this.Top = rnd.Next(marginY, (int)workingArea.Height - marginY - (int)NotificationBody.Height);
            }));

            TextField.Text = title;
            NotificationBackground.Background = ColorList[rnd.Next(0,4)];
        }
    }
}
