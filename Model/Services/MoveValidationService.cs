using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Services
{
    public class MoveValidationService
    {
        public List<Cell> GetPossibleMoves(GameField field, IPlayer player)
        {
            return field.GetNeighbours(player.CurrentCell)
                .SelectMany(cell => GetMovesOneDirection(player.CurrentCell, cell, field)).ToList();
        }


        private (int, int) OverOneCell(Cell start, Cell end)
        {
            return (2 * end.X - start.X, 2 * end.Y - start.Y);
        }
        private List<Cell> Occupied(Cell start, Cell end, GameField field)
        {
            List<Cell> neighbours = field.GetNeighbours(end);
            neighbours.Remove(start);
            Cell res = neighbours.FirstOrDefault(cell => (cell.X, cell.Y) == OverOneCell(start, end));
            return res == null || !CanMoveBetween(end, res, field)
                ? neighbours.Where(cell => CanMoveBetween(end, cell, field)).ToList()
                : new List<Cell> { res };
        }

        private List<Cell> GetMovesOneDirection(Cell start, Cell end, GameField field)
        {
            if (!CanMoveBetween(start, end, field)) return new List<Cell>();
            if (!end.HasPlayer) return new List<Cell> { end };
            return Occupied(start, end, field);
        }

        private Cell MaxCell(Cell first, Cell second)
        {
            if (first.X > second.X || first.Y > second.Y) return first;
            return second;
        }

        private bool IsXAxis(Cell first, Cell second)
        {
            return first.Y == second.Y;
        }

        public bool CanMoveBetween(Cell first, Cell second, GameField field)
        {
            Cell max = MaxCell(first, second);
            return !field.Corners[max.X, max.Y]
                .Obstacles[IsXAxis(first, second) ? 1 : 2, IsXAxis(first, second) ? 0 : 1];
        }
    }
}
