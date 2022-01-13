using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Server {
    public class TurnHandler {
        public async Task Handle(MakeTurn turn, List<Room> rooms) {
            await rooms.Find(x => x.RoomNumber == turn.RoomName).HandleTurn(turn);
        }
    }
}