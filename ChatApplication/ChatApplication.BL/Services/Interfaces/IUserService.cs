using ChatApplication.Contracts.Models.UiModels;

namespace ChatApplication.BL.Services.Interfaces;

public interface IUserService
{
    bool Register(User user);
    bool Login(string login, string password);
    bool Logout();
    User GetUserData();
}