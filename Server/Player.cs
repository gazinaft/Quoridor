using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using static BinProtocol.StreamTransmitter;

namespace Server
{
    public class Player {

        public Player(TcpClient client)
        {
            Client = client;
            stream = client.GetStream();
        }

        public TcpClient Client { get; }
        private NetworkStream stream;

        public bool Readable => Client.Available > 0;

        public async Task<byte[]> ReadAsync() {
            return await ReadFromStreamAsync(stream);
        }

        public async Task WriteAsync(byte[] data) {
            await WriteToStreamAsync(data, stream);
        }
        
    }
}