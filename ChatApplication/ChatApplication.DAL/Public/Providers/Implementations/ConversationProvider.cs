using ChatApplication.Contracts.Models.UiModels;
using ChatApplication.DAL.Db.Context;
using ChatApplication.DAL.Db.Repositories;
using ChatApplication.DAL.Mappers;
using ChatApplication.DAL.Models.DbModels;
using ChatApplication.DAL.Public.Providers.Interfaces;

namespace ChatApplication.DAL.Public.Providers.Implementations;

public class ConversationProvider : IConversationProvider
{
    private readonly ConversationRepository _conversationRepository;

    public ConversationProvider(ApplicationContext context)
    {
        _conversationRepository = new ConversationRepository(context);
    }

    public Conversation? GetOrCreateConversationId(Guid userId)
    {
        return _conversationRepository.GetOrCreateUserConversation(userId)
            .ToUiModel();
    }

    public List<ConversationMessage>? GetMessages(Guid conversationId)
    {
        return _conversationRepository.GetMessages(conversationId)
            .Select(x => x.ToUiModel()).ToList();
    }

    public bool AddMessage(Guid conversationId, ConversationMessageEntity message)
    {
        return _conversationRepository.AddMessageToConversation(conversationId, message);
    }

}