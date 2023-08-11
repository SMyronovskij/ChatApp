using System.Collections.ObjectModel;
using ChatApplication.BL.Services.Interfaces;
using ChatApplication.Contracts.Models.UiModels;
using CommunityToolkit.Mvvm.Input;

namespace ChatApplication.ViewModels;

public class ChatViewModel : BaseViewModel
{
    private readonly IConversationService _conversationService;
    private readonly ILoggerService _loggerService;
    private string _logs;
    private string _newMessageText;

    public Action<ConversationMessage> MessageAdded;
    private bool _isConnected;
    private string _connectionStatusText;

    public ChatViewModel(IConversationService conversationService, ILoggerService loggerService)
    {
        _conversationService = conversationService;
        _loggerService = loggerService;
        _loggerService.OnChatLog += log => { Logs += $"\n{log}"; };

        Messages = new ObservableCollection<ConversationMessage>();

        ConnectDisconnectCommand = new RelayCommand(ConnectDisconnect);
        SendCommand = new RelayCommand(Send);

        ConnectionStatusText = "Disconnected";

        Logs = string.Empty;
    }

    public ObservableCollection<ConversationMessage> Messages { get; set; }

    public RelayCommand ConnectDisconnectCommand { get; set; }
    public RelayCommand SendCommand { get; set; }

    public bool IsConnected
    {
        get => _isConnected;
        set
        {
            SetProperty(ref _isConnected, value);
            ConnectionStatusText = value ? "Connected" : "Disconnected";
        }
    }

    public string ConnectionStatusText
    {
        get => _connectionStatusText;
        set => SetProperty(ref _connectionStatusText, value);
    }

    public string Logs
    {
        get => _logs;
        set => SetProperty(ref _logs, value, nameof(Logs));
    }

    public string NewMessageText
    {
        get => _newMessageText;
        set => SetProperty(ref _newMessageText, value, nameof(NewMessageText));
    }

    private void Send()
    {
        var message = new ConversationMessage
        {
            Message = NewMessageText,
            CreationDateTime = DateTime.Now
        };

        _conversationService.SendMessage(message);
        NewMessageText = string.Empty;
    }

    private void Disconnect()
    {
        Logs += "\nDisconnected";
        IsConnected = !_conversationService.Disconnect();
    }

    private void Connect()
    {
        IsConnected = _conversationService.Connect();

        if (IsConnected)
        {
            Logs += "\nConnected";

            var messages = _conversationService.GetConversationMessages();

            foreach (var conversationMessage in messages)
            {
                Messages.Add(conversationMessage);
            }

            _conversationService.OnMessageReceived += conversationMessage =>
            {
                Messages.Add(conversationMessage);
                MainThread.BeginInvokeOnMainThread(() => MessageAdded?.Invoke(Messages.Last()));
            };
        }
    }

    private void ConnectDisconnect()
    {
        if (IsConnected)
        {
            Disconnect();
        }
        else
        {
            Connect();
        }
    }
}