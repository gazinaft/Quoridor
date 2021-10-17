using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class UserPlayer : IPlayer
    {
        public int PlayerId { get; }

        private Cell _currentCell;

        public PlayerState State { get; set; }
        public bool PlayerIsActive { get; set; }
        public int VictoryRow { get; set; }
        public Cell CurrentCell { get { return _currentCell; } set { _currentCell = value; _currentCell.HasPlayer = true; } }
        public int WallsCounter { get; set; }
        public IPlayerStrategy PlayerStrategy { get; set; }
        public ICommand LastStep { get; set; }

        public UserPlayer()
        {
            WallsCounter = 10;
        }

        public void Decide(Game game)
        {
            PlayerStrategy.Think(game);
        }
    }
}
