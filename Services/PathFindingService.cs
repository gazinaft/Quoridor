using Model;
using Services.Strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
