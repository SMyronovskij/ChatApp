using ChatApplication.Pages;

namespace ChatApplication;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        var mainPageName = nameof(MainPage);

        Routing.RegisterRoute($"{mainPageName}", typeof(MainPage));
        Routing.RegisterRoute($"{mainPageName}/{nameof(ChatPage)}", typeof(ChatPage));
        Routing.RegisterRoute($"{mainPageName}/{nameof(RegisterPage)}", typeof(RegisterPage));
    }
}