using System.Net.Sockets;

namespace Server
{
    using System.Collections.Generic;

    public class JoinHandler
    {
        public List<Room> Rooms;
        private Queue<Player> _waitingClients;

        public JoinHandler()
        {
            _waitingClients = new Queue<Player>();
            Rooms = new List<Room>();
        }


        public void Handle(Player client)
        {
            _waitingClients.Enqueue(client);
            TryCreateNewRoom();
        }

        private void TryCreateNewRoom()
        {
            if (_waitingClients.Count >= 2)
            {
                var players = new List<Player>()
                {
                    _waitingClients.Dequeue(),
                    _waitingClients.Dequeue()
                };
                var newRoom = new Room(Rooms.Count, players);
                newRoom.OnRoomClose += CloseRoom;
                Rooms.Add(newRoom);
            }
        }

        private void CloseRoom(Room closedRoom)
        {
            Rooms.Remove(closedRoom);
        }
    }
}