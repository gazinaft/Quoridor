using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace BinProtocol {
    public static class StreamTransmitter {
    
        public static async Task<byte[]> ReadFromStreamAsync(NetworkStream stream) {
            var packetSize = new byte[4];
            await stream.ReadAsync(packetSize, 0, packetSize.Length);
            var size = BitConverter.ToInt32(packetSize, 0);
            var data = new byte[size];
            await stream.ReadAsync(data, 0, size);
            return data;
        }
    
        public static async Task WriteToStreamAsync(byte[] data, NetworkStream stream) {
            var sendSize = BitConverter.GetBytes(data.Length);
            await stream.WriteAsync(sendSize, 0, 4);
            await stream.WriteAsync(data, 0, data.Length);
            await stream.FlushAsync();
        }
    
        public static void WriteToStream(byte[] data, NetworkStream stream) {
            var sendSize = BitConverter.GetBytes(data.Length);
            stream.Write(sendSize, 0, 4);
            stream.Write(data, 0, data.Length);
            stream.Flush();
        }
        
        public static byte[] ReadFromStream(NetworkStream stream) {
            var packetSize = new byte[4];
            stream.Read(packetSize, 0, packetSize.Length);
            var size = BitConverter.ToInt32(packetSize, 0);
            var data = new byte[size];
            stream.Read(data, 0, size);
            return data;
        }

    }
}
