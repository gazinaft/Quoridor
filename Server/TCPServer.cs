using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Server {
    public class TCPServer {

        private readonly TcpListener _listener;
        private JoinHandler _joinHandler;
        private List<TcpClient> _connections;
        private MessageHandler _messageHandler;
        
        public TCPServer() {
            _listener = new TcpListener(IPAddress.Any, 3000);
            _listener.Start();
            _joinHandler = new JoinHandler();
            _connections = new List<TcpClient>();
        }
        
        public async Task ListenJoining() {
            var token = new CancellationTokenSource();
            while (!token.IsCancellationRequested) {
                var client = await _listener.AcceptTcpClientAsync();
                _joinHandler.Handle(client);
            }
        }

        public async Task ListenMessages() {
            var token = new CancellationTokenSource();
            while (!token.IsCancellationRequested) {
                foreach (var client in _connections) {
                    // _mesageHandler.Handle();
                };
            }
        }
        
    }
}