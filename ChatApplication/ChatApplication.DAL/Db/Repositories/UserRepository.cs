using ChatApplication.DAL.Db.Context;
using ChatApplication.DAL.Models.DbModels;

namespace ChatApplication.DAL.Db.Repositories;

public class UserRepository
{
    private readonly ApplicationContext _context;

    public UserRepository(ApplicationContext _context)
    {
        this._context = _context;
    }

    public UserEntity? GetUserByLogin(string login)
    {
        return _context.Users
            .FirstOrDefault(u => u.Login.Equals(login));
    }

    public UserEntity RegisterUser(UserEntity user)
    {
        var userEntity = _context.Users.Add(user);

        var conversation = new ConversationEntity
        {
            User = userEntity.Entity
        };
        _context.Conversations.Add(conversation);

        _context.SaveChangesAsync();
        return userEntity.Entity;
    }
}