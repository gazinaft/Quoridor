using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using View;
using Model;
namespace MappingProj
{
    public class GameFieldMapper
    {
        public GameFieldState FromModelToView(GameField model) {

            return new GameFieldState() { GridForCorners = model.FormGridForObstacles(), GridForPlayers = model.FormGridForPlayers() };
        
        }
    }
}
