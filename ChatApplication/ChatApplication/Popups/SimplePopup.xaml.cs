using CommunityToolkit.Maui.Views;

namespace ChatApplication.Popups;

public partial class SimplePopup : Popup
{
    public SimplePopup(string text)
    {
        InitializeComponent();

        TextLabel.Text = text;
    }

    private async void OnOKButtonClicked(object? sender, EventArgs e)
    {
        await CloseAsync();
    }
}