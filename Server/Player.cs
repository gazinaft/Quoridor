using System.Net.Sockets;

namespace Server
{
    public class Player
    {
        public Player(TcpClient client)
        {
            Client = client;
        }

        public TcpClient Client { get; }
    }
}