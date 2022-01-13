using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using static BinProtocol.StreamTransmitter;

namespace Server
{
    public class TCPServer
    {
        private readonly TcpListener _listener;
        private JoinHandler _joinHandler;
        private List<Player> _connections;
        // private List<Player> _messageConnections;
        private MessageHandler _messageHandler;
        private CancellationTokenSource cancelToken;
        
        public TCPServer()
        {
            _listener = new TcpListener(IPAddress.Any, 3000);
            _listener.Start();
            _joinHandler = new JoinHandler();
            _connections = new List<Player>();
            // _messageConnections = new List<Player>();
            _messageHandler = new MessageHandler(new TurnHandler());
            cancelToken = new CancellationTokenSource();
        }

        public async Task ListenJoining() {
            ListenMessages();
            while (true)
            {
                var client = await _listener.AcceptTcpClientAsync();
                var player = new Player(client);
                _connections.Add(player);
                _joinHandler.Handle(player);
            }
        }

        public async Task ListenMessages() {
            while (true) {
                await Task.Delay(50);
                foreach (var player in _connections.Where(x => x.Client.Available > 0)) {
                    var data = await player.ReadAsync();
                    await _messageHandler.Handle(data, _joinHandler.Rooms);
                }
            }
            
        }

        public void Start() {
            ListenJoining();
            ListenMessages();
        }

        public void Stop() {
            cancelToken.Cancel();
        }
        
    }
}