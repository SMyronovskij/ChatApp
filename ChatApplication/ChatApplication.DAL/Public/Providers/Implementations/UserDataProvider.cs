using ChatApplication.Contracts.Models.UiModels;
using ChatApplication.DAL.Db.Context;
using ChatApplication.DAL.Db.Repositories;
using ChatApplication.DAL.Mappers;
using ChatApplication.DAL.Models.DbModels;
using ChatApplication.DAL.Public.Providers.Interfaces;

namespace ChatApplication.DAL.Public.Providers.Implementations;

public class UserDataProvider : IUserDataProvider
{
    private readonly UserRepository _userRepository;
    public UserDataProvider(ApplicationContext context)
    {
        _userRepository = new UserRepository(context);
    }


    public User? GetUserByLogin(string login)
    {
        return _userRepository?.GetUserByLogin(login)?.ToUiModel();
    }

    public User? RegisterUser(UserEntity user)
    {
        return _userRepository
            .RegisterUser(user)
            .ToUiModel();
    }
}