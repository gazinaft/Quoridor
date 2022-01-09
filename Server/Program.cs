namespace Server
{
    using System;
    using System.Net.Sockets;

    internal class Program
    {
        public static void Main(string[] args)
        {
            var tcpServ = new TCPServer();
            tcpServ.ListenJoining();

            Console.ReadLine();
        }
    }
}