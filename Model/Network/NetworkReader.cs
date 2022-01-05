using System;
using System.Net.Sockets;
using Google.Protobuf;
using static BinProtocol.StreamTransmitter;

namespace Model.Network {
    public class NetworkReader {

        public bool GetTurnOrder(NetworkStream stream) {
            var req = new General {
                Turn = new MakeTurn {
                X = 1,
                Y = 2,
                IsHorizontal = false,
                RoomName = 4,
                ToPlaceWall = false
            } }.ToByteArray();
            WriteToStreamSync(req, stream);
            var data = ReadFromStreamSync(stream);
            var turn = MakeTurn.Parser.ParseFrom(data);
            return !turn.IsHorizontal;
        }
    }
}