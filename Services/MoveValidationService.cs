using System.Collections.Generic;
using Model;

namespace Services
{
    public class MoveValidationService
    {
        public List<Cell> GetPossibleMoves(GameField field, IPlayer player) {

            List<Cell> currentNeighbours = new List<Cell>();

            if (field.Cells[player.CurrentCell.X + 1, player.CurrentCell.Y] != null) {

                currentNeighbours.Add(field.Cells[player.CurrentCell.X + 1, player.CurrentCell.Y]);
            
            }
            if (field.Cells[player.CurrentCell.X - 1, player.CurrentCell.Y] != null)
            {

                currentNeighbours.Add(field.Cells[player.CurrentCell.X - 1, player.CurrentCell.Y]);

            }
            if (field.Cells[player.CurrentCell.X, player.CurrentCell.Y + 1] != null)
            {

                currentNeighbours.Add(field.Cells[player.CurrentCell.X, player.CurrentCell.Y + 1]);

            }
            if (field.Cells[player.CurrentCell.X, player.CurrentCell.Y - 1] != null)
            {

                currentNeighbours.Add(field.Cells[player.CurrentCell.X, player.CurrentCell.Y - 1]);

            }

            return currentNeighbours.FindAll(neighbour => CanMoveBetween(player.CurrentCell, neighbour, field));
        }
        
        private Cell MaxCell(Cell first, Cell second) {
            if (first.X > second.X || first.Y > second.Y) return first;
            return second;
        }

        private bool IsXAxis(Cell first, Cell second) {
            return first.Y == second.Y;
        }


        public bool CanMoveBetween(Cell first, Cell second, GameField field) {
            Cell max = MaxCell(first, second);
            return field.Corners[max.X, max.Y]
                .Obstacles[IsXAxis(first, second) ? 1 : 2, IsXAxis(first, second) ? 0 : 1];
        }
        
        

    }
}
