using ChatApplication.BL.Services.Interfaces;
using ChatApplication.Contracts.Models.UiModels;

namespace ChatApplication.BL.Services.Implementations;

public class UserService : IUserService
{
    private readonly IAuthorizationService _authorizationService;

    private User? _currentUser;

    public UserService(IAuthorizationService authorizationService)
    {
        _authorizationService = authorizationService;
    }

    public bool Register(User user)
    {
        var registeredUser = _authorizationService.Register(user);
        return registeredUser != null;
    }

    public bool Login(string login, string password)
    {
        _currentUser = _authorizationService.Login(login, password);
        return _currentUser != null;
    }

    public bool Logout()
    {
        _currentUser = null;
        return true;
    }

    public User GetUserData()
    {
        return _currentUser ?? throw new Exception("User is not logged in");
    }
}