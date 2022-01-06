using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Protobuf;
using System.Net.Sockets;
using BinProtocol;

namespace Server
{
    public class Room
    {
        public event Action<Room> OnRoomClose;
        private readonly List<Player> _players;
        private Player WhitePlayer => _players[_white];
        private Player BlackPlayer => _players[1 - _white];
        private bool _firstPlayerTurn = true;
        public int RoomNumber;
        private readonly int _white;
        private const int width = 8;
        private const int height = 8;
        private Player Receiver => _firstPlayerTurn ? BlackPlayer : WhitePlayer;

        public Room(int roomNumber, List<Player> players)
        {
            RoomNumber = roomNumber;
            _players = players;
            _white = new Random().Next(0, 2);

            StartGame();
        }

        public async Task HandleTurn(MakeTurn input)
        {
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

        private async Task SendTurn(MakeTurn turn)
        {
            var general = new General() {Turn = turn};
            var msg = general.ToByteArray();
            try
            {
                await StreamTransmitter.WriteToStreamAsync(msg, Receiver.Client.GetStream());
                if (turn.IsLastTurn)
                {
                    foreach (var player in _players)
                    {
                        player.Client.Close();
                        player.Client.Dispose();
                        OnRoomClose?.Invoke(this);
                    }
                }

                StartListening(Receiver.Client.GetStream());
                _firstPlayerTurn = !_firstPlayerTurn;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error sending turn " + e.Message);
            }
        }

        public async Task StartGame()
        {
            var startWhite = new StartGame
            {
                IsFirst = true
            };

            var general = new General() {StartGame = startWhite};
            var msg = general.ToByteArray();

            var wStream = WhitePlayer.Client.GetStream();
            await StreamTransmitter.WriteToStreamAsync(msg, wStream);

            var startBlack = new StartGame
            {
                IsFirst = false
            };

            general = new General() {StartGame = startBlack};
            msg = general.ToByteArray();

            var bStream = BlackPlayer.Client.GetStream();
            await StreamTransmitter.WriteToStreamAsync(msg, bStream);


            await StartListening(WhitePlayer.Client.GetStream());
        }

        private async Task StartListening(NetworkStream stream)
        {
            while (true)
            {
                var data = await StreamTransmitter.ReadFromStreamAsync(stream);
                var msg = General.Parser.ParseFrom(data);
                MakeTurn turn;
                if (msg.MsgCase == General.MsgOneofCase.Turn)
                {
                    turn = msg.Turn;
                    await HandleTurn(turn);
                    return;
                }
            }
        }
    }
}