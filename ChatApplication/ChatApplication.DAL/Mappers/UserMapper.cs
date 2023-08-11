using ChatApplication.Contracts.Models.UiModels;
using ChatApplication.DAL.Models.DbModels;

namespace ChatApplication.DAL.Mappers;

public static class UserMapper
{
    public static User? ToUiModel(this UserEntity userDto)
    {
        if (userDto == null)
            return null;

        return new User
        {
            Id = userDto.Id,
            Login = userDto.Login,
            FirstName = userDto.FirstName,
            LastName = userDto.LastName,
            Password = userDto.Password
        };
    }

    public static UserEntity ToDbModel(this User user)
    {
        if (user == null)
            return null;

        return new UserEntity
        {
            Id = user.Id,
            Login = user.Login,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Password = user.Password
        };
    }
}