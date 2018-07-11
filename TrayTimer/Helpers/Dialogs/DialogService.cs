namespace TrayTimer.Helpers
{
    using System.Windows;

    public class DialogService : IDialogService
    {
        private bool isMessageBoxOpened = false;

        public void ShowErrorMessageBox(string message)
        {
            if (!isMessageBoxOpened)
                return;
            
            MessageBox.Show(message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            isMessageBoxOpened = true;
            
        }

        public void ShowInfoMessageBox(string message)
        {
            if (isMessageBoxOpened)
                return;
            
            MessageBox.Show(message, "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
            isMessageBoxOpened = true;
        }
    }
}
