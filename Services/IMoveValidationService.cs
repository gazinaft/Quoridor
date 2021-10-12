using System.Collections.Generic;
using Model;
namespace Services
{
    public interface IMoveValidationService
    {
        bool CanMoveBetween(Cell firstCell, Cell secondCell, GameField field);

        List<Cell> GetPossibleMoves(GameField field, IPlayer player);
    }
}
