using ChatApplication.Contracts.Models.UiModels;

namespace ChatApplication.BL.Services.Interfaces;

public interface IConversationService
{
    public Action<ConversationMessage> OnMessageReceived { get; set; }
    bool Connect();
    bool Disconnect();

    void SendMessage(ConversationMessage message);

    public List<ConversationMessage> GetConversationMessages();
}