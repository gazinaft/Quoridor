using System.Net.Sockets;

namespace Server
{
    using System.Collections.Generic;

    public class JoinHandler
    {
        private List<Room> _rooms;
        private Queue<TcpClient> _waitingClients;

        public JoinHandler()
        {
            _waitingClients = new Queue<TcpClient>();
            _rooms = new List<Room>();
        }


        public void Handle(TcpClient client)
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
                    new Player(_waitingClients.Dequeue()),
                    new Player(_waitingClients.Dequeue())
                };
                var newRoom = new Room(_rooms.Count, players);
                newRoom.OnRoomClose += CloseRoom;
                _rooms.Add(newRoom);
            }
        }

        private void CloseRoom(Room closedRoom)
        {
            _rooms.Remove(closedRoom);
        }
    }
}