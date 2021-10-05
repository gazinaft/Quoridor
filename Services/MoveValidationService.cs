using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Services
{
    public class MoveValidationService
    {
        public List<Cell> GetPossibleMoves(GameField field, IPlayer player) {

            List<Cell> possibleMoves = new List<Cell>();

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

            possibleMoves = currentNeighbours.FindAll(neighbour => CanMoveBetween(player.CurrentCell, neighbour, field));

            return possibleMoves;
            
        }

        


        public bool CanMoveBetween(Cell firstCell, Cell secondCell, GameField field) {

            if (firstCell.X == secondCell.X)
            {

                if (firstCell.Y > secondCell.Y)
                {

                        return !(field.Corners[firstCell.X, secondCell.Y].Obstacles[2, 1] == 1);

                }
                else
                {

                        return !(field.Corners[secondCell.X, firstCell.Y].Obstacles[2, 1] == 1);

                }

            }
            else {

                if (firstCell.X > secondCell.Y)
                {

                    
                        return !(field.Corners[firstCell.X, secondCell.Y].Obstacles[1, 0] == 1);


                }
                else {


                        return !(field.Corners[secondCell.X, firstCell.Y].Obstacles[1, 0] == 1);
                   

                }
            
            }
        
        }

    }
}
