using ChatApplication.Contracts.Models.UiModels;

namespace ChatApplication.BL.Services.Interfaces;

public interface IConversationService
{
    bool Connect();
    bool Disconnect();

    void SendMessage(ConversationMessage message);

    public Action<ConversationMessage> OnMessageReceived { get; set; }

    public List<ConversationMessage> GetConversationMessages();
}