using ChatApplication.BL.Services.Interfaces;
using ChatApplication.Contracts.Models.UiModels;
using ChatApplication.Contracts.Service;
using ChatApplication.DAL.Mappers;
using ChatApplication.DAL.Public.Providers.Interfaces;

namespace ChatApplication.BL.Services.Implementations;

public class AuthorizationService : IAuthorizationService
{
    private readonly IEncryptionService _encryptionService;
    private readonly IUserDataProvider _userDataProvider;

    public AuthorizationService(
        IUserDataProvider userDataProvider,
        IEncryptionService encryptionService)
    {
        _userDataProvider = userDataProvider;
        _encryptionService = encryptionService;
    }

    public User? Register(User user)
    {
        var entity = user.ToDbModel();

        var hashedPass = _encryptionService.GetHashedString(entity.Password);
        entity.Password = hashedPass;

        return _userDataProvider.RegisterUser(entity);
    }

    public User? Login(string login, string password)
    {
        var userDto = _userDataProvider.GetUserByLogin(login);

        if (userDto == null)
            return null;

        var hashSumString = _encryptionService.GetHashedString(password);

        return userDto.Login == login && userDto.Password == hashSumString
            ? userDto
            : null;
    }
}