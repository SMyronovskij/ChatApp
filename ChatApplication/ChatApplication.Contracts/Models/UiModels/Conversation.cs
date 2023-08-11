namespace ChatApplication.Contracts.Models.UiModels;

public class Conversation
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public User User { get; set; }

    public List<ConversationMessage> ConversationMessages { get; set; }
}