using ChatApplication.Contracts.Models.Dtos;

namespace ChatApplication.Contracts.Models.UiModels;

public class ConversationMessage
{
    public Guid Id { get; set; }

    public Guid ClientId { get; set; }
    public string ClientName { get; set; }

    public string Message { get; set; }

    public DateTime CreationDateTime { get; set; }

    public bool IsMine { get; set; }

    public ConversationMessageDto ToDto(Guid clientId)
    {
        var conversationMessageDto = new ConversationMessageDto
        {
            Message = Message,
            CreationDateTime = CreationDateTime.Ticks,
            ClientId = ClientId,
            ClientName = ClientName
        };

        if (conversationMessageDto.ClientId == null) conversationMessageDto.ClientId = clientId;

        return conversationMessageDto;
    }
}