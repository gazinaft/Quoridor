using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model {
    public interface IPlayer {
        public int PlayerId { get; }
        
        public void Think(GameField field);
        public void Decide();
    }
}
