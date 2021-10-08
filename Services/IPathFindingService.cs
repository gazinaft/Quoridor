using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Services.Strategy;
namespace Services
{
    public interface IPathFindingService
    {
        bool CanPlaceTheWall(GameField field, IPlayer player);

        IAlgorithm SelectedAlgorithm { get; set; }

    }
}
