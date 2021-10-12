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
