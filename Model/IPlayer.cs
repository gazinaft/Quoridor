using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model {
    public interface IPlayer {
        
        public int PlayerId { get; }

        public PlayerState State { get; set; }

        public bool PlayerIsActive { get; set; }

        public int VictoryRow { get; set; }

        public Cell CurrentCell { get; set; }

        public int WallsCounter { get; set; }
        
        public IPlayerStrategy PlayerStrategy { get; set; } // таким чином не будемо переписувати реалізацію гравця в залежності від зміни алгоритму

        public void Decide();
    }

    public enum PlayerState
    {

        ChangeTheCell,
        PlaceTheWall

    }
}
