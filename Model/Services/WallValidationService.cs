using System;
using System.Collections.Generic;
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

        public List<(Corner, bool)> GetPossibleWalls(GameField field, List<IPlayer> players, Cell radius)
        {
            var res = new List<(Corner, bool)>();
            int dx = 3;

            for (int x = Math.Max(1, radius.X - dx); x < Math.Min(field.Width, radius.X + dx); x++)
            {
                for (int y = Math.Max(1, radius.Y - dx); y < Math.Min(field.Height, radius.Y + dx); y++)
                {
                    if (!CornerInvalid(x, y, true, field, players)) res.Add((field.Corners[x, y], true));
                    if (!CornerInvalid(x, y, false, field, players)) res.Add((field.Corners[x, y], false));
                }
            }

            return res;
        }

        public bool CornersAreEmpty(List<IPlayer> players) {

            IPlayer activePlayer = players.Find(pl => pl.PlayerIsActive);

            return activePlayer.WallsCounter <= 0;
        }

        public bool CornerInvalid(int x, int y, bool isHorizontal, GameField field, List<IPlayer> players)
        {
            if (CornersAreEmpty(players)) return true;

            if (isHorizontal) {
                return field.Corners[x, y].Obstacles[0, 1] ||
                       field.Corners[x, y].Obstacles[1, 1] ||
                       field.Corners[x, y].Obstacles[2, 1] ||
                       field.Corners[x + 1, y].Obstacles[0, 1] ||
                       field.Corners[x - 1, y].Obstacles[2, 1] ||
                       HasNoPath(x, y, true, field, players);
            }
            return field.Corners[x, y].Obstacles[1, 0] ||
                   field.Corners[x, y].Obstacles[1, 1] ||
                   field.Corners[x, y].Obstacles[1, 2] ||
                   field.Corners[x, y + 1].Obstacles[1, 0] ||
                   field.Corners[x, y - 1].Obstacles[1, 2] ||
                   HasNoPath(x, y, false, field, players);
        }

        private bool HasNoPath(int x, int y, bool isHorizontal, GameField field, List<IPlayer> players) {
            field.SetBlock(x, y, isHorizontal);
            for (var i = 0; i < players.Count; i++) {
                if (_pathFindingService.SelectedAlgorithm.FindThePath(players[i], field).Count == 0) {
                    field.SetBlock(x, y, isHorizontal, false);
                    return true;
                }
            }

            field.SetBlock(x, y, isHorizontal, false);
            return false;
        }
        
    }
}
