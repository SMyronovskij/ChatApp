using System.ComponentModel.DataAnnotations;

namespace ChatApplication.DAL.Models.DbModels;

public class ConversationEntity
{
    [Key] public Guid Id { get; set; }

    public Guid UserId { get; set; }
    public UserEntity User { get; set; }

    public List<ConversationMessageEntity> ConversationMessages { get; set; }
}