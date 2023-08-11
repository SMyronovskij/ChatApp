using ChatApplication.Contracts.Models.UiModels;
using ChatApplication.DAL.Models.DbModels;

namespace ChatApplication.DAL.Public.Providers.Interfaces;

public interface IConversationProvider
{
    Conversation? GetOrCreateConversationId(Guid userId);
    List<ConversationMessage>? GetMessages(Guid conversationId);
    bool AddMessage(Guid conversationId, ConversationMessageEntity message);
}