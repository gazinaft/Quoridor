using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Strategy;
namespace Model.Services
{
    public class PathFindingService
    {
        public IAlgorithm SelectedAlgorithm { get; set; }

        bool CanPlaceTheWall(GameField field, IPlayer player)
        {
            throw new NotImplementedException();
        }

    }
}
