using System;

namespace Server {
    public class MessageHandler {

        public MessageHandler(
            object TurnHandler,
            object JoinHandler,
            object EndGageHandler) {
            
            
        }
        
        public void Handle(byte[] raw) {
            var data = General.Parser.ParseFrom(raw);
            switch (data.MsgCase) {
                case General.MsgOneofCase.None:
                    return;
                case General.MsgOneofCase.EndGame:
                    return;
                case General.MsgOneofCase.Turn:
                    break;
                case General.MsgOneofCase.StartGame:
                    break;
                case General.MsgOneofCase.Joined:
                    break;
                default: throw new ArgumentException("Unknown message");
            }
        }
    }
}