using ClientServerArchitecture.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;



namespace ClientServerArchitecture.Server
{
    public class QuoridorServer
    {
        private byte[] _buffer;
        
        private Socket _serverSocket;

        private List<Socket> _clientSockets;

        public QuoridorServer()
        {
            _clientSockets = new List<Socket>();

            _buffer = new byte[1024];
            
        }

        private void SetupServer()
        {
            _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Udp);

            _serverSocket.Bind(new IPEndPoint(IPAddress.Any, 100));

            _serverSocket.Listen(5);

            _serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
        }

        private void AcceptCallback(IAsyncResult result) 
        {
            Socket socket = _serverSocket.EndAccept(result);

            _clientSockets.Add(socket);

            socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), socket);

            _serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);

        }

        private void ReceiveCallback(IAsyncResult result) 
        {
            Socket socket = (Socket)result.AsyncState;

            int received = socket.EndReceive(result);

            byte[] dataBuffer = new byte[received];

            Array.Copy(_buffer, dataBuffer, received);

            string text = Encoding.ASCII.GetString(dataBuffer);

            ReceiveJson(text);


        }

        private void SendCallback(IAsyncResult result) 
        {
            Socket socket = (Socket)result.AsyncState;

            socket.EndSend(result);
            
        }

        private void StartGame(IClient firstClient, IClient secondClient) 
        {

        }

        private void ReceiveJson(string JsonData) 
        {

            var message = JsonConvert.DeserializeObject<IMessage>(JsonData);

        }

        private void SendJson(IMessage message, Socket socket) 
        {
            var JsonObject = JsonConvert.SerializeObject(message);

            byte[] data = Encoding.ASCII.GetBytes(JsonObject);

            socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(SendCallback), socket);

        }

    }
}
