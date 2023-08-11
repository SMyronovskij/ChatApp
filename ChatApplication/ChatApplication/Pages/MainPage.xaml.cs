using ChatApplication.ViewModels;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace ChatApplication;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        BindingContext = Ioc.Default.GetRequiredService<MainViewModel>();
    }

    private async void OnCounterClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainPage/ChatPage");
    }
}