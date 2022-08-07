namespace A3ServerTool;

/// <summary>
/// Shows exceptions in fancy MahApps Metro message box
/// </summary>
public sealed class ExceptionHandler
{
    static ExceptionHandler() {}
    private ExceptionHandler() {}

    public static ExceptionHandler Instance { get; } = new ExceptionHandler();

    public async void ShowMessage(Exception exception)
    {
        var dialogSettings = new MetroDialogSettings
        {
            AffirmativeButtonText = "OK",
            ColorScheme = MetroDialogColorScheme.Accented
        };

        Trace.TraceError(exception.Message);
        var method = new StackTrace(exception).GetFrame(0).GetMethod().Name;

        await ((MetroWindow)System.Windows.Application.Current.MainWindow)
            .ShowMessageAsync("Warning!", exception.Message + "\n" + "Exception caused by " + method, MessageDialogStyle.Affirmative, dialogSettings);
    }
}
