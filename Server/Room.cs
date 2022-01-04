using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Protobuf;

namespace Server {
    public class Room {
        private readonly List<Player> _players;
        private Player WhitePlayer => _players[_white];
        private Player BlackPlayer => _players[1 - _white];
        private bool _firstPlayerTurn = true;
        public int RoomNumber;
        private readonly int _white;
        private const int width = 8;
        private const int height = 8;
        private Player Receiver => _firstPlayerTurn ? BlackPlayer : WhitePlayer;
        
        public Room(int roomNumber, List<Player> players) {
            RoomNumber = roomNumber;
            _players = players;
            _white = new Random().Next(0, 2);
        }

        public async Task HandleTurn(MakeTurn input) {
            var res = new MakeTurn
            {
                RoomName = input.RoomName,
                ToPlaceWall = input.ToPlaceWall,
                X = width - input.X,
                Y = height - input.Y,
                IsHorizontal = input.IsHorizontal
            };
            await SendTurn(res);
        }

        private async Task SendTurn(MakeTurn turn) {
            var data = turn.ToByteArray();
            try {
                await Receiver.client.GetStream().WriteAsync(data, 0, data.Length);
                _firstPlayerTurn = false;
            }
            catch (Exception e) {
                Console.WriteLine("Error sending turn " + e.Message);
            }
        }

        public async Task StartGame() {
            var startWhite = new StartGame {
                IsFirst = true
            }.ToByteArray();
            var fStream = WhitePlayer.client.GetStream();
            await fStream.WriteAsync(startWhite, 0, startWhite.Length);
            
            var startBlack = new StartGame {
                IsFirst = false
            }.ToByteArray();
            var bStream = BlackPlayer.client.GetStream();
            await bStream.WriteAsync(startBlack, 0, startBlack.Length);
        }

    }
}