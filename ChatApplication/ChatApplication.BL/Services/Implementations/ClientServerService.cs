using System.Net;
using System.Net.Sockets;
using System.Text;
using ChatApplication.BL.Services.Interfaces;

namespace ChatApplication.BL.Services.Implementations;

public class ClientServerService : IClientServerService
{
    private const int Port = 5000;
    private static readonly object Lock = new();
    private static readonly Dictionary<int, TcpClient> list_clients = new();
    private readonly ILoggerService _loggerService;

    private readonly IPAddress IpAddress = IPAddress.Parse("127.0.0.1");
    private bool _serverOwned;
    private TcpListener? _tcpListener;

    public ClientServerService(ILoggerService loggerService)
    {
        _loggerService = loggerService;
    }

    public Action<string> OnMessageReceived { get; set; }
    public bool ServerIsUp { get; set; }
    public bool ClientConnected { get; set; }

    #region Server

    public bool StartServer()
    {
        if (ServerIsUp) return false;

        if (_tcpListener == null)
            _tcpListener = new TcpListener(IpAddress, Port);

        try
        {
            _tcpListener.Start();
            _serverOwned = true;
        }
        catch (Exception e)
        {
            _loggerService.Log(e.Message, LogType.ErrorLog);
            ServerIsUp = true;
            _serverOwned = false;
            _tcpListener = null;
            return false;
        }

        if (_tcpListener == null) return false;

        Task.Run(() =>
        {
            var count = 1;

            while (ServerIsUp)
                try
                {
                    var client = _tcpListener.AcceptTcpClient();
                    lock (Lock)
                    {
                        list_clients.Add(count, client);
                    }

                    _loggerService.Log("Someone connected!!", LogType.ChatLog);

                    var thread = new Thread(HandleClients);
                    thread.Start(count);
                    count++;
                }
                catch
                {
                    // ignored
                }
        });

        ServerIsUp = true;
        return true;
    }

    public void HandleClients(object o)
    {
        var id = (int)o;
        TcpClient client;

        lock (Lock)
        {
            client = list_clients[id];
        }

        while (ServerIsUp)
            try
            {
                var stream = client.GetStream();
                var buffer = new byte[1024];
                var byte_count = stream.Read(buffer, 0, buffer.Length);

                if (byte_count == 0) break;

                var data = Encoding.ASCII.GetString(buffer, 0, byte_count);
                Broadcast(data);
            }
            catch
            {
                // ignored
            }
    }

    public void ShutdownServer()
    {
        if (!ServerIsUp || !_serverOwned) return;
        ServerIsUp = false;

        foreach (var client in list_clients.Values)
        {
            client.Client.Shutdown(SocketShutdown.Both);
            client.Close();
        }

        /*lock (_lock)
        {
            list_clients.Remove(client);
        }*/
        _tcpListener.Stop();
    }

    public static void Broadcast(string data)
    {
        var buffer = Encoding.ASCII.GetBytes(data + Environment.NewLine);

        lock (Lock)
        {
            foreach (var stream in list_clients.Values.Select(c => c.GetStream()))
                stream.Write(buffer, 0, buffer.Length);
        }
    }

    #endregion

    #region Client

    private NetworkStream? _networkStream;
    private TcpClient? _client;
    private Thread? _clientThread;

    public bool StartClient(string clientName)
    {
        if (ClientConnected) return false;

        try
        {
            _client = new TcpClient();
            _client.Connect(IpAddress, Port);
            _loggerService.Log($"Client connected: {clientName}", LogType.ChatLog);
            _networkStream = _client.GetStream();
            _clientThread = new Thread(client => ReceiveData((TcpClient)client));
            _clientThread.Start(_client);
        }
        catch (Exception e)
        {
            return false;
        }

        ClientConnected = true;
        return true;
    }

    public void ShutdownClient()
    {
        if (!ClientConnected) return;
        if (_client == null || _clientThread == null || _networkStream == null) return;

        _client.Client.Shutdown(SocketShutdown.Send);
        _clientThread.Join();
        _networkStream.Close();
        _client.Close();

        _client = null;
        _clientThread = null;
        _networkStream = null;

        _loggerService.Log("Disconnect from server!!", LogType.ChatLog);

        ClientConnected = false;
    }

    public void SendMessage(string message)
    {
        if (!ClientConnected) return;

        var buffer = Encoding.ASCII.GetBytes(message);
        _networkStream?.Write(buffer, 0, buffer.Length);
    }

    private void ReceiveData(TcpClient client)
    {
        var ns = client.GetStream();
        var receivedBytes = new byte[1024];
        int byte_count;

        while ((byte_count = ns.Read(receivedBytes, 0, receivedBytes.Length)) > 0)
            OnMessageReceived?.Invoke(Encoding.ASCII.GetString(receivedBytes, 0, byte_count));
    }

    #endregion
}