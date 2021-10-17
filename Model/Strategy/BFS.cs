using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Model.Strategy
{
    public class BFS : IAlgorithm
    {

        public List<Cell> FindThePath(IPlayer player, GameField field)
        {

            Queue<Cell> queue = new Queue<Cell>();

            Dictionary<Cell, Cell> parents = new Dictionary<Cell, Cell>();

            List<Cell> visited = new List<Cell>();

            Cell currentCell = player.CurrentCell;

            visited.Add(currentCell);

            queue.Enqueue(currentCell);

            while (queue.Any())
            {
                var V = queue.Dequeue();

                foreach (Cell cell in field.GetNeighbours(V).FindAll(c => field.CanMoveBetween(V, c, field)))
                {

                    if (!visited.Contains(cell))
                    {
                        parents.Add(cell, V);
                        queue.Enqueue(cell);

                        if (cell.Y == player.VictoryRow) return BFSResult(cell, parents);

                        visited.Add(cell);

                    }

                }

            }
            return new List<Cell>();

        }

        private List<Cell> BFSResult(Cell endCell, Dictionary<Cell, Cell> curParents) {

            Cell temp = endCell;

            List<Cell> finalList = new List<Cell> { temp };
            
            while (curParents.ContainsKey(temp)) {
                
                finalList.Add(curParents[temp]);
                temp = curParents[temp];

            }
            return finalList;
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

            for (int i = 0; i < field.Cells.GetLength(0); i++)
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
