using ChatApplication.Contracts.Models.UiModels;

namespace ChatApplication.Contracts.Models.Dtos;

public class ConversationMessageDto
{
    public Guid ClientId { get; set; }
    public string ClientName { get; set; }

    public string Message { get; set; }

    public long CreationDateTime { get; set; }

    public ConversationMessage ToUiModel(Guid clientId)
    {
        return new ConversationMessage
        {
            Message = Message,
            CreationDateTime = new DateTime(CreationDateTime),
            ClientId = ClientId,
            ClientName = ClientName,
            IsMine = ClientId == clientId
        };
    }
}