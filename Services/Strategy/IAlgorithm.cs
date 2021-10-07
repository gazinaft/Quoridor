using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
namespace Services.Strategy
{
    public interface IAlgorithm
    {
        List<Cell> FindThePath(IPlayer player, GameField field);
    }
}
