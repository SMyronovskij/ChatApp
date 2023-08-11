using System.Text.Json;
using ChatApplication.BL.Services.Interfaces;
using ChatApplication.Contracts.Models.Dtos;
using ChatApplication.Contracts.Models.UiModels;
using ChatApplication.DAL.Mappers;
using ChatApplication.DAL.Public.Providers.Interfaces;

namespace ChatApplication.BL.Services.Implementations;

public class ConversationService : IConversationService
{
    private readonly IClientServerService _clientServerService;
    private readonly IConversationProvider _conversationProvider;
    private readonly ILoggerService _loggerService;
    private readonly IUserService _userService;

    private Conversation? _conversation;

    public ConversationService(IClientServerService clientServerService,
        IUserService userService, ILoggerService loggerService,
        IConversationProvider conversationProvider)
    {
        _clientServerService = clientServerService;
        _userService = userService;
        _loggerService = loggerService;
        _conversationProvider = conversationProvider;
    }

    public bool Connect()
    {
        if (_clientServerService.ClientConnected)
            return true;

        _loggerService.Log("Connected", LogType.ChatLog);

        var serverStarted = _clientServerService.StartServer();
        _loggerService.Log(serverStarted ? "Server started" : "Can not start server, already running", LogType.ChatLog);

        _clientServerService.StartClient(_userService.GetUserData().Login);

        _clientServerService.OnMessageReceived += ClientServerServiceOnMessageReceived;

        _conversation = _conversationProvider.GetOrCreateConversationId(_userService.GetUserData().Id);
        return true;
    }

    public bool Disconnect()
    {
        _loggerService.Log("Disconnected", LogType.ChatLog);

        if (_clientServerService.ServerIsUp)
            _clientServerService.ShutdownServer();


        if (!_clientServerService.ClientConnected)
            return true;

        _clientServerService.OnMessageReceived -= ClientServerServiceOnMessageReceived;
        _clientServerService.ShutdownClient();

        return true;
    }

    public void SendMessage(ConversationMessage message)
    {
        if(!_clientServerService.ClientConnected)
            return;

        message.ClientId = _userService.GetUserData().Id;
        message.ClientName = _userService.GetUserData().Login;

        var model = message.ToDto(_userService.GetUserData().Id);
        var json = JsonSerializer.Serialize(model);

        _clientServerService.SendMessage(json);
    }

    public Action<ConversationMessage> OnMessageReceived { get; set; }

    private void ClientServerServiceOnMessageReceived(string message)
    {
        _loggerService.Log($"Message received: {message}", LogType.ChatLog);

        var model = JsonSerializer.Deserialize<ConversationMessageDto>(message);
        var conversationMessage = model.ToUiModel(_userService.GetUserData().Id);

        _conversationProvider.AddMessage(_conversation.Id, conversationMessage.ToDbModel());

        OnMessageReceived?.Invoke(conversationMessage);
    }

    public List<ConversationMessage> GetConversationMessages()
    {
        if(_conversation == null)
            return new List<ConversationMessage>();
            
        var messages = _conversationProvider.GetMessages(_conversation.Id);

        foreach (var conversationMessage in messages)
        {
            conversationMessage.IsMine = conversationMessage.ClientId == _userService.GetUserData().Id;
        }

        return messages;
    }
}