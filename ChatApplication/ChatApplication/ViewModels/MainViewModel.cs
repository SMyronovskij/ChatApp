using ChatApplication.BL.Services.Interfaces;
using ChatApplication.Popups;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Input;

namespace ChatApplication.ViewModels;

public class MainViewModel : BaseViewModel
{
    private readonly IUserService _userService;

    public MainViewModel(IUserService userService)
    {
        _userService = userService;

        LoginCommand = new AsyncRelayCommand(Authorize);
        RegistrationCommand = new AsyncRelayCommand(Registration);
    }

    private async Task Registration()
    {
        await Shell.Current.GoToAsync("//MainPage/RegisterPage");
    }

    public string Login { get; set; }

    public string Password { get; set; }

    public AsyncRelayCommand LoginCommand { get; set; }
    public AsyncRelayCommand RegistrationCommand { get; set; }

    private async Task Authorize()
    {
        var loggedIn = _userService.Login(Login, Password);

        if (!loggedIn)
        {
            await Shell.Current.CurrentPage
                .ShowPopupAsync(new SimplePopup("Wrong login or password"));
            return;
        }

        await Shell.Current.GoToAsync("//MainPage/ChatPage");
    }
}