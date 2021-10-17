using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Strategy;
namespace Model.Services
{
    public class WallValidationService
    {
        public PathFindingService _pathFindingService;

        public WallValidationService(PathFindingService pfs)
        {
            _pathFindingService = pfs;

            _pathFindingService.SelectedAlgorithm = new BFS();
        }

        private List<(Corner, bool)> GetPossibleWalls(GameField field, IPlayer player)
        {
            var res = new List<(Corner, bool)>();
            for (int x = 1; x < field.Width; x++)
            {
                for (int y = 1; y < field.Height; y++)
                {
                    if (!CornerInvalid(x, y, true, field, player)) res.Add((field.Corners[x, y], true));
                    if (!CornerInvalid(x, y, false, field, player)) res.Add((field.Corners[x, y], false));
                }
            }

            return res;
        }

        public bool CornerInvalid(int x, int y, bool isHorizontal, GameField field, IPlayer player)
        {
            if (isHorizontal) {
                return field.Corners[x, y].Obstacles[0, 1] ||
                       field.Corners[x, y].Obstacles[1, 1] ||
                       field.Corners[x, y].Obstacles[2, 1] ||
                       field.Corners[x + 1, y].Obstacles[0, 1] ||
                       field.Corners[x - 1, y].Obstacles[2, 1] ||
                       HasPath(x, y, true, field, player);
            }
            return field.Corners[x, y].Obstacles[1, 0] ||
                   field.Corners[x, y].Obstacles[1, 1] ||
                   field.Corners[x, y].Obstacles[1, 2] ||
                   field.Corners[x, y + 1].Obstacles[1, 0] ||
                   field.Corners[x, y - 1].Obstacles[1, 2] ||
                   HasPath(x, y, false, field, player);
        }

        private bool HasPath(int x, int y, bool isHorizontal, GameField field, IPlayer player) {
            field.SetBlock(x, y, isHorizontal);
            bool res = _pathFindingService.SelectedAlgorithm.FindThePath(player, field).Count == 0;
            field.SetBlock(x, y, isHorizontal, false);
            return res;
        }
        
    }
}
