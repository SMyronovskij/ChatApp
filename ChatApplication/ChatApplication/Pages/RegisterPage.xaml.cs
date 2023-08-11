using ChatApplication.ViewModels;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace ChatApplication.Pages;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
        BindingContext = Ioc.Default.GetRequiredService<RegisterViewModel>();
    }
}