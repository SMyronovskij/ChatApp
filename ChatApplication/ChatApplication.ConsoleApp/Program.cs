// See https://aka.ms/new-console-template for more information


using ChatApplication.BL.Services.Implementations;
using ChatApplication.Contracts.Service;
using ChatApplication.DAL.Public.Providers.Implementations;

Console.WriteLine("Hello, World!");

Console.Write("Login: ");
var login = Console.ReadLine();
Console.Write("Password: ");
var password = Console.ReadLine();

var authorizationService = new AuthorizationService(new UserDataProvider(), new EncryptionService());
var user = authorizationService.Login(login, password);

/*if (user != null)
{
    Console.WriteLine($"Hello, {user.FirstName} {user.LastName}!");

    var clientServerService = new ClientServerService();
    try
    {
        clientServerService.InitServer();
    }
    catch
    {
        // ignored
    }
    finally
    {
        clientServerService.InitClient();
    }
}
else
{
    Console.WriteLine("Invalid login or password!");
    
}*/