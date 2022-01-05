using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using Google.Protobuf;

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
            // RoomNumber = start.RoomName;
            return start;
        }
        
        public void Think(Game game) {
            
            var lastStep = game._stepsHistory.Last();
            var (x, y, isHorizontal) = lastStep.InfoForSerialize();
            var toSend = new MakeTurn {
                ToPlaceWall = lastStep is PlaceWallCommand,
                X = x,
                Y = y,
                IsHorizontal = isHorizontal
            }.ToByteArray();
            stream.Write(toSend, 0, toSend.Length);

            
        }

    }
}