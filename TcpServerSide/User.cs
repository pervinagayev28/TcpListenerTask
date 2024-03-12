// Server Side => TcpListener

using System.Net.Sockets;

internal class User
{
    public string UserName { get; set; }
    public TcpClient client{ get; set; }
}