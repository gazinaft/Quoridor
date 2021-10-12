using View;
using Model;
namespace MappingProj
{
    public class GameFieldMapper
    {
        public GameFieldState FromModelToView(GameField model) {

            return new GameFieldState() { GridForCorners = model.FormGridForObstacles(), GridForPlayers = model.FormGridForPlayers(), Height = model.Height, Width = model.Width };
        
        }
    }
}
