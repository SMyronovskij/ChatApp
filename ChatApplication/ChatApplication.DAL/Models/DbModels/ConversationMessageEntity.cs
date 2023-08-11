using System.ComponentModel.DataAnnotations;

namespace ChatApplication.DAL.Models.DbModels;

public class ConversationMessageEntity
{
    [Key] public Guid Id { get; set; }

    [Required] public string Message { get; set; }

    [Required] public DateTime CreationDateTime { get; set; }

    public string ClientName { get; set; }

    public Guid ClientId { get; set; }

    public Guid ConversationId { get; set; }

    public ConversationEntity Conversation { get; set; }
}