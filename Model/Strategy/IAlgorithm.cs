using System.Collections.Generic;

namespace Model.Strategy
{
    public interface IAlgorithm
    {
        List<Cell> FindThePath(IPlayer player, GameField field);
    }
}
