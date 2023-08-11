using ChatApplication.Contracts.Models.UiModels;
using ChatApplication.DAL.Models.DbModels;

namespace ChatApplication.DAL.Mappers;

public static class ConversationMapper
{
    public static Conversation? ToUiModel(this ConversationEntity conversationDto)
    {
        if (conversationDto == null)
            return null;

        return new Conversation
        {
            User = conversationDto.User?.ToUiModel(),
            Id = conversationDto.Id,
            UserId = conversationDto.UserId,
            ConversationMessages = conversationDto.ConversationMessages?
                .Select(x => x.ToUiModel()).ToList()
        };
    }

    public static ConversationEntity? ToDbModel(this Conversation conversation)
    {
        if (conversation == null)
            return null;

        return new ConversationEntity
        {
            User = conversation.User?.ToDbModel(),
            Id = conversation.Id,
            UserId = conversation.UserId,
            ConversationMessages = conversation.ConversationMessages?
                .Select(x => x.ToDbModel()).ToList()
        };
    }

    public static ConversationMessage? ToUiModel(this ConversationMessageEntity conversationMessageDto)
    {
        if (conversationMessageDto == null)
            return null;

        return new ConversationMessage
        {
            Id = conversationMessageDto.Id,
            Message = conversationMessageDto.Message,
            CreationDateTime = conversationMessageDto.CreationDateTime,
            ClientId = conversationMessageDto.ClientId,
            ClientName = conversationMessageDto.ClientName
        };
    }

    public static ConversationMessageEntity ToDbModel(this ConversationMessage conversation)
    {
        if (conversation == null)
            return null;

        return new ConversationMessageEntity
        {
            Id = conversation.Id,
            Message = conversation.Message,
            CreationDateTime = conversation.CreationDateTime,
            ClientId = conversation.ClientId,
            ClientName = conversation.ClientName
        };
    }
}