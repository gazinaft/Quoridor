using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
namespace Services
{
    public interface IMoveValidationService
    {
        bool CanMoveBetween(Cell firstCell, Cell secondCell, GameField field);

        List<Cell> GetPossibleMoves(GameField field, IPlayer player);
    }
}
