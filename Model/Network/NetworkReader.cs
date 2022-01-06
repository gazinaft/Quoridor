using System;
using System.Net.Sockets;
using Google.Protobuf;
using static BinProtocol.StreamTransmitter;

namespace Model.Network {
    public class NetworkReader {

        public StartGame GetTurnOrder(NetworkStream stream) {
            var req = new General {
                Joined = new JoinedToQueue {
                    Success = true
            } }.ToByteArray();
            WriteToStreamSync(req, stream);
            var data = ReadFromStreamSync(stream);
            var start = StartGame.Parser.ParseFrom(data);
            return start;
        }
    }
}