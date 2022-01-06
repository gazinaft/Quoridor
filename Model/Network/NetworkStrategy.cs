﻿using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using Google.Protobuf;
using static BinProtocol.StreamTransmitter;

namespace Model.Network {
    
    public class NetworkStrategy: IPlayerStrategy {

        const int PORT = 3000;

        private TcpClient conn;
        private NetworkStream stream;
        private NetworkReader nReader;
        public int RoomNumber;
        
        public NetworkStrategy(NetworkReader nReader) {
            this.nReader = nReader;
            conn = new TcpClient();
            conn.Connect(IPAddress.Parse("127.0.0.1"), PORT);
            stream = conn.GetStream();
        }

        public bool IsFirstTurn() {
            var start =  nReader.GetTurnOrder(stream);
            RoomNumber = start.RoomName;
            return start.IsFirst;
        }
        
        public void Think(Game game) {
            
            var lastStep = game._stepsHistory.Last();
            var (x, y, isHorizontal) = lastStep.InfoForSerialize();
            var toSend = new MakeTurn {
                ToPlaceWall = lastStep is PlaceWallCommand,
                X = x,
                Y = y,
                IsHorizontal = isHorizontal,
                IsLastTurn = false
            }.ToByteArray();
            WriteToStreamSync(toSend, stream);
            var bitNextTurn = ReadFromStreamSync(stream);
            var next = MakeTurn.Parser.ParseFrom(bitNextTurn);
            if (next.ToPlaceWall) {
                new PlaceWallCommand(next.X, next.Y, next.IsHorizontal).Execute(game);
            }
            else {
                var nextCell = game.Board.Cells[next.X, next.Y];
                new MovePlayerCommand(nextCell).Execute(game);
            }
        }

        public void SendVictory(Game game) {
            var lastStep = game._stepsHistory.Last();
            var (x, y, isHorizontal) = lastStep.InfoForSerialize();
            var toSend = new MakeTurn {
                ToPlaceWall = lastStep is PlaceWallCommand,
                X = x,
                Y = y,
                IsHorizontal = isHorizontal,
                IsLastTurn = true
            }.ToByteArray();
            WriteToStreamSync(toSend, stream);
        }

    }
}