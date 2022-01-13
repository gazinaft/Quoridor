using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server {
    public class MessageHandler {

        private readonly TurnHandler turnHandler;

        public MessageHandler(TurnHandler th) {
            turnHandler = th;
        }
        
        public async Task Handle(byte[] raw, List<Room> rooms) {
            Console.WriteLine("Handling message");
            var data = General.Parser.ParseFrom(raw);
            switch (data.MsgCase) {
                case General.MsgOneofCase.None:
                    return;
                case General.MsgOneofCase.Turn:
                    await turnHandler.Handle(data.Turn, rooms);
                    break;
                default: throw new ArgumentException("Unknown message");
            }
        }
    }
}