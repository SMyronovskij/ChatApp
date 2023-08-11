using ChatApplication.BL.Services.Interfaces;
using ChatApplication.Contracts.Models.UiModels;
using ChatApplication.Popups;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Input;

namespace ChatApplication.ViewModels;

public class RegisterViewModel : BaseViewModel
{
    private readonly IUserService _userService;

    public RegisterViewModel(IUserService userService)
    {
        _userService = userService;

        RegisterCommand = new AsyncRelayCommand(RegisterAsync);
    }

    public string Login { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Password { get; set; }

    public AsyncRelayCommand RegisterCommand { get; set; }

    private async Task RegisterAsync()
    {
        if (string.IsNullOrEmpty(Login)
            || string.IsNullOrEmpty(Password)
            || string.IsNullOrEmpty(FirstName)
            || string.IsNullOrEmpty(LastName))
        {
            await Shell.Current.CurrentPage
                .ShowPopupAsync(new SimplePopup("Not all fields are filled"));
            return;
        }

        var registered = _userService.Register(new User
        {
            Login = Login,
            FirstName = FirstName,
            LastName = LastName,
            Password = Password
        });

        if (registered)
            await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
        else
            await Shell.Current.CurrentPage
                .ShowPopupAsync(new SimplePopup("Registration failed"));
    }
}