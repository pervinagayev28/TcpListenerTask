// Client Side => TcpClient

using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Channels;


// Clinetin Connect olacaqi Ip ve Port
var ip = IPAddress.Parse("127.0.0.1");
var port = 27001;

// Clientin Conntect olacaqi EndPoint
var serverEP = new IPEndPoint(ip, port);


var client = new TcpClient();

// Servere Connect oluruq
client.Connect(serverEP);

// Servere Data gondermek

NetworkStream? clientStream = client.GetStream();
BinaryWriter? writer = new BinaryWriter(clientStream);
BinaryReader? reader = new BinaryReader(clientStream);


// Message Oxumaq
_ = Task.Run(() =>
{
    while (true)
    {
        Console.WriteLine(reader.ReadString());
    }
});


// Message Gondermek
while (true)
{
    var message = Console.ReadLine();
    writer.Write(message);
}