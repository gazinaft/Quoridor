using View;
using Model;
using System.Collections.Generic;

namespace MappingProj
{
    public class GameFieldMapper
    {
        public GameFieldState FromModelToView(Game model) {

            GameFieldState result = new GameFieldState() { GridForCorners = model.Board.FormGridForObstacles(), GridForPlayers = model.Board.FormGridForPlayers(), Height = model.Board.Height, Width = model.Board.Width, CurrentPlayerID = model.ActivePlayer.PlayerId };

            List<(int, int)> playersStates = new List<(int, int)>();
            
            
            model.Players.ForEach(pl => playersStates.Add((pl.PlayerId, pl.WallsCounter)));

            result._playersStates = playersStates;

            return result;
        }
    }
}
