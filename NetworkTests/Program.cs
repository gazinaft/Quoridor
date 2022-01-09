using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Google.Protobuf;
using static BinProtocol.StreamTransmitter;

namespace NetworkTests {
    internal class Program {
        public static void Main(string[] args) {
            var server = new TcpListener(IPAddress.Parse("127.0.0.1"), 3000);
            server.Start();
        
            var token = new CancellationTokenSource();
            Console.WriteLine("Started listening");
        
            _ = Task.Run(async () => {
        
                while (!token.IsCancellationRequested) {
                    var client = await server.AcceptTcpClientAsync();
                    await Answer(client);
                }
            }, token.Token);
            Console.ReadLine();
        }
        
        
         public static async Task Answer(TcpClient client) {
            Console.WriteLine("Working with client");
            var stream = client.GetStream();
            var bytes = await ReadFromStreamAsync(stream);
            Console.WriteLine("Read data successfully");
            var general = General.Parser.ParseFrom(bytes);
            Console.WriteLine(general.ToString());
            // if (general.MsgCase != General.MsgOneofCase.Turn) return;
            var response = new MakeTurn {
                RoomName = 1,
                IsHorizontal = true,
                ToPlaceWall = false,
                X = 1,
                Y = 2
            }.ToByteArray();
            await WriteToStreamAsync(response, stream);
            // var sendByteSize = BitConverter.GetBytes(4);
            // await stream.WriteAsync(sendByteSize, 0, sendByteSize.Length);
            // await stream.WriteAsync(response, 4, response.Length);
            // await stream.FlushAsync();
         }

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