using ChatApplication.Contracts.Models.UiModels;

namespace ChatApplication.BL.Services.Interfaces;

public interface IAuthorizationService
{
    User? Register(User user);
    User? Login(string login, string password);
}