using Model;
using Services.Strategy;
using System;

namespace Services
{
    public class PathFindingService : IPathFindingService
    {
        public IAlgorithm SelectedAlgorithm { get; set; }

        bool CanPlaceTheWall(GameField field, IPlayer player)
        {
            throw new NotImplementedException();
        }

        bool IPathFindingService.CanPlaceTheWall(GameField field, IPlayer player)
        {
            throw new NotImplementedException();
        }
    }
}
