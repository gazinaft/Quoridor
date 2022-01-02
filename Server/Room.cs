using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;
using Google.Protobuf;

namespace Server {
    public class Room {
        private readonly Player _firstPlayer;
        private readonly Player _secondPlayer;
        private bool firstPlayerTurn = true;
        private int _roomNumber;
        Player Receiver => firstPlayerTurn ? _secondPlayer : _firstPlayer;
        
        public Room(int roomNumber, List<Player> players) {
            _roomNumber = roomNumber;
            var first = new Random().Next(0, 2);
            _firstPlayer = players[first];
            _secondPlayer = players[1 - first];
        }
        
        public void SendTurn(MakeTurn turn) {
            var data = turn.ToByteArray();
            try {
                Receiver.client.GetStream().WriteAsync(data, 0, data.Length);
                firstPlayerTurn = false;
            }
            catch (Exception e) {
                Console.WriteLine("Error sending turn " + e.Message);
            }
        }

        public void StartGame() {
            var startWhite = new StartGame {
                IsFirst = true
            }.ToByteArray();
            
            var startBlack = new StartGame {
                IsFirst = false
            }.ToByteArray();
        }
        
        

    }
}