namespace TrayTimer.Helpers
{
    using System;

    public interface IDispatcherTimer
    {
        void Start(int timeStamp, EventHandler action);
        void Stop();
    }
}
