using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public abstract class Player : IPlayer
    {
        public int PlayerId => throw new NotImplementedException();

        public PlayerState State { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool PlayerIsActive { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int VictoryRow { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Cell CurrentCell { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int WallsCounter { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IPlayerStrategy PlayerStrategy { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ICommand LastStep { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Decide()
        {
            throw new NotImplementedException();
        }
    }
}
