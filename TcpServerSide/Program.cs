// Server Side => TcpListener

using System.Net;
using System.Net.Sockets;
using System.Text;


var ip = IPAddress.Parse("127.0.0.1");
var port = 27001;


var listenerEP = new IPEndPoint(ip, port);
BinaryReader br;
BinaryWriter bw;

var listener = new TcpListener(listenerEP);
listener.Start();

Console.WriteLine($"{listener.Server.LocalEndPoint}  Listener Started .....");


var clients = new List<User>();

while (true)
{
    TcpClient client = listener.AcceptTcpClient();
    _ = Task.Run(() =>
    {
        br = new(client.GetStream());
        var user = clients.FirstOrDefault(c => c.client.Client?.RemoteEndPoint == client.Client.RemoteEndPoint);
        if (user is null)
        {
            bw = new(client.GetStream());
            bw.Write("enter your name please: ");
            var username = br.ReadString();
            user = new()
            {
                UserName = username,
                client = client
            };
            clients.Add(user);
        }

        Console.WriteLine(user?.UserName + " connected...");

        var clientStream = client.GetStream();
        var reader = new BinaryReader(clientStream);


        var readString = "";
        var userIpEndPoint = "";
        var index = 0;


        while (true)
        {
            readString = reader.ReadString();//Name
            TcpClient? client = clients.FirstOrDefault(c => readString.Contains(c.UserName))?.client;
            if (client is not null)
            {
                var writer = new BinaryWriter(client.GetStream());
                writer.Write(readString.Substring(readString.IndexOf(" ") + 1));
            }
        }
    });
}