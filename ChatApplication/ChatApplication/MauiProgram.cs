using ChatApplication.BL.Services.Implementations;
using ChatApplication.BL.Services.Interfaces;
using ChatApplication.Contracts.Service;
using ChatApplication.DAL.Db.Context;
using ChatApplication.DAL.Public.Providers.Implementations;
using ChatApplication.DAL.Public.Providers.Interfaces;
using ChatApplication.ViewModels;
using CommunityToolkit.Maui;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ChatApplication;

public static class MauiProgram
{
    private const string DbName = "chatApp.db";
    private static bool _initialized;

    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder()
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .RegisterDb();
        //.RegisterAppServices()
        //.RegisterViewModels();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        if (!_initialized)
        {
            _initialized = true;
            Ioc.Default.ConfigureServices(
                new ServiceCollection()
                    //Services
                    .AddSingleton<IUserDataProvider, UserDataProvider>()
                    .AddSingleton<IConversationProvider, ConversationProvider>()
                    .AddSingleton<IAuthorizationService, AuthorizationService>()
                    .AddSingleton<IEncryptionService, EncryptionService>()
                    .AddSingleton<IClientServerService, ClientServerService>()
                    .AddSingleton<ILoggerService, LoggerService>()
                    .AddSingleton<IUserService, UserService>()
                    .AddSingleton<IConversationService, ConversationService>()
                    .AddSingleton<ApplicationContext>()
                    //ViewModels
                    .AddTransient<MainViewModel>()
                    .AddTransient<ChatViewModel>()
                    .AddTransient<RegisterViewModel>()
                    .BuildServiceProvider());
        }

        return builder.Build();
    }


    public static MauiAppBuilder RegisterDb(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddDbContext<ApplicationContext>();
        return mauiAppBuilder;
    }
}