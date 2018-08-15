namespace TrayTimer.Helpers
{
    public interface IDialogService
    {
        void ShowErrorMessageBox(string message);
        void ShowInfoMessageBox(string message);
    }
}