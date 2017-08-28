﻿using System;
using System.Windows;
using System.Windows.Media;

namespace TrayTimer
{
    public partial class Notification : Window
    {
        public int NotificationClicks = 0;
        private int marginX = 150;
        private int marginY = 20;
        private int numberOfEscapes = 0;
        private string notificationTitle;

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
            notificationTitle = title;
            numberOfEscapes = rnd.Next(0, 3) == 0 ? rnd.Next(1, 3) : 0; // 50% change for not escaping at all
            DrawPosition();
        }

        private void DrawPosition()
        {
            var workingArea = System.Windows.SystemParameters.WorkArea;
            this.Left = rnd.Next(marginX, (int)workingArea.Width - marginX - (int)NotificationBody.Width);
            this.Top = rnd.Next(marginY, (int)workingArea.Height - marginY - (int)NotificationBody.Height);
            
            TextField.Text = String.Format("{0} ({1})", notificationTitle, numberOfEscapes);
            NotificationBackground.Background = ColorList[rnd.Next(0, 4)];
        }

        private void NotificationClick(object sender, System.Windows.Input.MouseButtonEventArgs e) { NotificationClicks = 1; }
        private void NotificationFocus(object sender, System.Windows.Input.MouseEventArgs e)
        {
            numberOfEscapes--;
            if (numberOfEscapes >= 0) { DrawPosition(); }
        }

    }
}
