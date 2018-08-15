namespace TrayTimer.Helpers
{
    using System;
    using System.Windows.Threading;

    public class DispatcherTimerWrapper : IDispatcherTimer
    {
        private readonly DispatcherTimer dispatcherTimer;

        public DispatcherTimerWrapper()
        {
            dispatcherTimer = new DispatcherTimer(DispatcherPriority.Send, Dispatcher.CurrentDispatcher);
        }

        public void Start(int timeStamp, EventHandler action)
        {
            dispatcherTimer.Interval = TimeSpan.FromSeconds(timeStamp);
            dispatcherTimer.Tick += action;
            dispatcherTimer.Start();
        }

        public void Stop()
        {
            dispatcherTimer.Stop();
        }
    }
}
