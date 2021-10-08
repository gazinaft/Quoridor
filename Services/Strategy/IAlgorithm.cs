using System.Collections.Generic;
using Model;
namespace Services.Strategy
{
    public interface IAlgorithm
    {
        List<Cell> FindThePath(IPlayer player, GameField field);
    }
}
