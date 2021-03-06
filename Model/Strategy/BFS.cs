using System.Collections.Generic;
using System.Linq;

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

                var all = FindAllCanMoveBetween(field.GetNeighbours(V), V, field);
                for (var i = 0; i < all.Count; i++) {
                    Cell cell = all[i];
                    if (!visited.Contains(cell)) {
                        parents.Add(cell, V);
                        queue.Enqueue(cell);

                        if (cell.Y == player.VictoryRow) return BFSResult(cell, parents);

                        visited.Add(cell);
                    }
                }
            }
            return new List<Cell>();

        }

        private List<Cell> FindAllCanMoveBetween(List<Cell> neighbours, Cell first, GameField field) {
            List<Cell> res = new List<Cell>();
            for (int i = 0; i < neighbours.Count; ++i) {
                var cell = neighbours[i];
                if (field.CanMoveBetween(first, cell, field)) {
                    res.Add(cell);
                }
            }

            return res;
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

    }
}
