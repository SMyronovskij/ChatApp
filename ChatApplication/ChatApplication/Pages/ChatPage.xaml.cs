using ChatApplication.ViewModels;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace ChatApplication.Pages;

public partial class ChatPage : ContentPage
{
    public ChatPage()
    {
        InitializeComponent();

        BindingContext = Ioc.Default.GetRequiredService<ChatViewModel>();

        var vm = (ChatViewModel)BindingContext;
        vm.MessageAdded += message => { MessagesCollectionView.ScrollTo(message); };
    }
}