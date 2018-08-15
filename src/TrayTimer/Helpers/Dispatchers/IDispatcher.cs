namespace TrayTimer.Helpers
{
    using System;

    public interface IDispatcher
    {
        void Invoke(Action action);
        void BeginInvoke(Action action);
    }
}
