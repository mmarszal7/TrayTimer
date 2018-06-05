using System;
using System.Windows.Media;

namespace TrayTimer.Helpers
{
    public sealed class ApplicationOptions
    {
        private static readonly object padlock = new object();
        private static ApplicationOptions instance = null;
        public static ApplicationOptions Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new ApplicationOptions();
                    }
                    return instance;
                }
            }
        }

        public string NotificationText { get; set; } = "Reminder";
        public double IntervalTime { get; set; } = 1;
        public bool IsColorRandom { get; set; }
        public DefaultLocation NotificationPosition { get; set; } = DefaultLocation.LowerRight;
        public Action OptionsChanged { get; set; }

        public readonly SolidColorBrush[] ColorList = new SolidColorBrush[5]
        {
            new SolidColorBrush(Colors.DarkRed),
            new SolidColorBrush(Colors.DarkBlue),
            new SolidColorBrush(Colors.DarkGreen),
            new SolidColorBrush(Colors.DarkOrange),
            new SolidColorBrush(Colors.DarkViolet)
        };
    }
}
