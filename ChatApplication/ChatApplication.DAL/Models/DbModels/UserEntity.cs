using System.ComponentModel.DataAnnotations;

namespace ChatApplication.DAL.Models.DbModels;

public class UserEntity
{
    [Key] public Guid Id { get; set; }

    [Required] public string Login { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    [Required] public string Password { get; set; }

    public ConversationEntity? Conversations { get; set; }
}