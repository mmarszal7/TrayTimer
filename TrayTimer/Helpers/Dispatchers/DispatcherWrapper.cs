namespace TrayTimer.Helpers
{
    using System;
    using System.Windows;
    using System.Windows.Threading;

    public class DispatcherWrapper : IDispatcher
    {
        private readonly Dispatcher dispatcher;

        public DispatcherWrapper()
        {
            dispatcher = Application.Current.Dispatcher;
        }
        public void Invoke(Action action)
        {
            dispatcher.Invoke(action);
        }

        public void BeginInvoke(Action action)
        {
            dispatcher.BeginInvoke(action);
        }
    }
}
