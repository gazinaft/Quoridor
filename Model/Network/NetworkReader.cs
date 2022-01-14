using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using Google.Protobuf;
using static BinProtocol.StreamTransmitter;

namespace Model.Network
{
    public class NetworkReader
    {
        public StartGame GetTurnOrder(NetworkStream stream)
        {
            // var req = new General {
            //     Joined = new JoinedToQueue {
            //         Success = true
            // } }.ToByteArray();
            // WriteToStreamSync(req, stream);
            var data = Task.Run(async () => await ReadFromStreamAsync(stream)).Result;
            var msg = General.Parser.ParseFrom(data);
            var start = msg.StartGame;
            return start;
        }
    }
}