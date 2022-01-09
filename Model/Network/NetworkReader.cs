using System;
using System.Net.Sockets;
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
            var data = ReadFromStream(stream);
            var msg = General.Parser.ParseFrom(data);
            var start = msg.StartGame;
            return start;
        }
    }
}