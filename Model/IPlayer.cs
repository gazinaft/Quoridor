using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model {
    public interface IPlayer {
        public int PlayerId { get; }

        private Cell MakeMove(GameField field) { }
        private (Corner, bool) SetBlock(GameField field) { }
        
        public Think(GameField field) { }
        public Decide(GameField field) { }
    }
}
