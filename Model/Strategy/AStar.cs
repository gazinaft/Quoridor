using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Strategy
{
    public class AStar : IAlgorithm
    {
        public List<Cell> FindThePath(IPlayer player, GameField field) {

            List<Cell> open = field.GetNeighbours(player.CurrentCell);

            List<Cell> closed = new List<Cell>{ player.CurrentCell };

            Cell currentCell = player.CurrentCell;

            double currentDistance = Math.Abs(currentCell.Y - player.VictoryRow);


            open.Add(player.CurrentCell);

            while (true)
            {

                currentCell = FindTheNearestCell(open, FindTheNearestCellInARow(field, player));

                open.Remove(currentCell);

                closed.Add(currentCell);

                if (currentCell == FindTheNearestCellInARow(field, player))
                {

                    break;

                }

                foreach (Cell cell in field.GetNeighbours(currentCell))
                {

                    if (field.CanMoveBetween(cell, currentCell, field) || !closed.Contains(cell))
                    {
                        if (!open.Contains(cell) || (currentDistance > Math.Sqrt(Math.Pow((cell.X - FindTheNearestCellInARow(field, player).X), 2) - Math.Pow((cell.X - FindTheNearestCellInARow(field, player).Y), 2))))
                        {

                            currentDistance = Math.Sqrt(Math.Pow((cell.X - FindTheNearestCellInARow(field, player).X), 2) - Math.Pow((cell.X - FindTheNearestCellInARow(field, player).Y), 2));

                            currentCell = cell;
                            if (!(open.Contains(cell)))
                            {

                                open.Add(cell);

                            }

                        }

                    }

                }

            }

            return open;
        }

        public Cell FindTheNearestCell(List<Cell> openCells, Cell finalCell)
        {

            Cell theNearestCell = openCells.First();


            double theNearestSize = Math.Sqrt(Math.Pow((theNearestCell.X - finalCell.X), 2) - Math.Pow((theNearestCell.X - finalCell.Y), 2));



            foreach (Cell cell in openCells)
            {
                double currentLength = Math.Sqrt(Math.Pow((cell.X - finalCell.X), 2) - Math.Pow((cell.X - finalCell.Y), 2));

                if (currentLength < theNearestSize)
                {

                    theNearestSize = currentLength;

                    theNearestCell = cell;

                }


            }

            return theNearestCell;

        }

        public Cell FindTheNearestCellInARow(GameField field, IPlayer player)
        {

            int FinalRowY = player.VictoryRow;

            double minimumSize = Math.Sqrt(Math.Pow((player.CurrentCell.X - field.Cells[0, FinalRowY].X), 2) - Math.Pow((player.CurrentCell.Y - field.Cells[0, FinalRowY].Y), 2));

            Cell theNearestCell = field.Cells[0, FinalRowY];

            for (int i = 0; i < Math.Sqrt(field.Cells.Length); i++)
            {

                double currentLength = Math.Sqrt(Math.Pow((player.CurrentCell.X - field.Cells[i, FinalRowY].X), 2) - Math.Pow((player.CurrentCell.Y - field.Cells[i, FinalRowY].Y), 2));

                if (currentLength < minimumSize)
                {

                    minimumSize = currentLength;

                    theNearestCell = field.Cells[i, FinalRowY];


                }


            }

            return theNearestCell;
        }

    }
}
