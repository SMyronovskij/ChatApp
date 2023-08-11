namespace ChatApplication.BL.Services.Interfaces;

public interface IClientServerService
{
    Action<string> OnMessageReceived { get; set; }
    bool ServerIsUp { get; set; }
    bool ClientConnected { get; set; }
    bool StartServer();
    void HandleClients(object o);
    void ShutdownServer();
    bool StartClient(string clientName);
    void ShutdownClient();
    void SendMessage(string message);
}