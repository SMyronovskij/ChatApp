using ChatApplication.Contracts.Models.UiModels;
using ChatApplication.DAL.Models.DbModels;

namespace ChatApplication.DAL.Public.Providers.Interfaces;

public interface IUserDataProvider
{
    User? GetUserByLogin(string login);
    User? RegisterUser(UserEntity user);
}