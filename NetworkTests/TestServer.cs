using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Google.Protobuf;
using static BinProtocol.StreamTransmitter;

namespace NetworkTests {
    internal class Program {

        private static List<TcpClient> clients = new List<TcpClient>();
        public static void Main(string[] args) {
            var server = new TcpListener(IPAddress.Parse("127.0.0.1"), 3000);
            server.Start();
        
            var token = new CancellationTokenSource();
            Console.WriteLine("Started listening");
        
            _ = Task.Run(async () => {
                Listen();
                while (!token.IsCancellationRequested) {
                    var client = await server.AcceptTcpClientAsync();
                    Console.WriteLine("Client added");
                    clients.Add(client);
                    // await Answer(client);
                }
            }, token.Token);
            Console.ReadLine();
        }

        public static async Task Listen() {
            try {
                while (true) {
                    await Task.Delay(2000);
                    foreach (var client in clients.Where(x => x.Available > 0)) {
                        var data = new byte[4];
                        client.NoDelay = true;
                        var stream = client.GetStream();
                        await stream.ReadAsync(data, 0, data.Length);
                        await stream.WriteAsync(data, 0, data.Length);

                    }
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex);
            }
        }
        
         // public static async Task Answer(TcpClient client) {
         //    Console.WriteLine("Working with client");
         //    var stream = client.GetStream();
         //    var bytes = await ReadFromStreamAsync(stream);
         //    Console.WriteLine("Read data successfully");
         //    var general = General.Parser.ParseFrom(bytes);
         //    Console.WriteLine(general.ToString());
         //    // if (general.MsgCase != General.MsgOneofCase.Turn) return;
         //    var response = new MakeTurn {
         //        RoomName = 1,
         //        IsHorizontal = true,
         //        ToPlaceWall = false,
         //        X = 1,
         //        Y = 2
         //    }.ToByteArray();
         //    await WriteToStreamAsync(response, stream);
         // }

        // public static void Main(string[] args) {
        //     General a = new General {
        //         EndGame = new EndGame {
        //             RoomName = 1
        //         }
        //     };
        //     Console.WriteLine("EndGame");
        //     Console.WriteLine(a.ToByteArray().Length);
        //     Console.WriteLine(a.CalculateSize());
        //     
        //     General b = new General {
        //         Joined = new JoinedToQueue {
        //             Success = true
        //         }
        //     };
        //     Console.WriteLine("JoinedToQueue");
        //     Console.WriteLine(b.ToByteArray().Length);
        //     Console.WriteLine(b.CalculateSize());
        //     
        //     General c = new General {
        //         Turn = new MakeTurn {
        //             IsHorizontal = false,
        //             RoomName = 1,
        //             ToPlaceWall = false,
        //             X = 1,
        //             Y = 2
        //         }
        //     };
        //     Console.WriteLine(c.ToByteArray().Length);
        //     Console.WriteLine(c.CalculateSize());
        //     
        //     General d = new General {
        //         EndGame = new EndGame {
        //             RoomName = 1
        //         }
        //     };
        //     Console.WriteLine(d.ToByteArray().Length);
        //     Console.WriteLine(d.CalculateSize());
        // }
    }
}