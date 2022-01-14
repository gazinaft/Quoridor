using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    using System;
    using System.Net.Sockets;

    internal class Program
    {
        public static void Main(string[] args)
        {
            var tcpServ = new TCPServer();
            var token = new CancellationTokenSource();
            Console.WriteLine("Started listening");

            Task.Run(async () => {
                await tcpServ.ListenJoining();
            });
            
            Console.ReadLine();
        }
    }
}