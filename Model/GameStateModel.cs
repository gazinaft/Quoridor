using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class GameStateModel
    {
        public IPlayer HasToWin { get; }

        public  IPlayer HasToLose { get; }

        public List<IPlayer> Players { get; set; }

        public GameField Board { get; set; }

        public GameStateModel(IPlayer firstP, IPlayer secondP, GameField board)
        {
            HasToWin = firstP;

            HasToLose = secondP;

            Board = board;

            Players = new List<IPlayer>();

            Players.Add(firstP);

            Players.Add(secondP);

        }

        public void MakeMove(int x, int y, IPlayer activePlayer) {

            Board.MovePlayer(x, y, activePlayer);
        
        }

        public void PlaceTheWall(int x, int y, bool isHorizontal, IPlayer activePlayer) {

            Board.SetBlock(x, y, isHorizontal);

            activePlayer.WallsCounter--;

        }

        public List<ICommand> GetLegalActions()
        {
            var res = new List<ICommand>();

            var list = Board.GetAvailableMoves(HasToWin);
            for (var i = 0; i < list.Count; i++)
            {
                res.Add(new MovePlayerCommand(list[i]));
            }

            foreach (var (c, isHorizontal) in Board.GetAvailableWalls(Players))
            {
                res.Add(new PlaceWallCommand(c.X, c.Y, isHorizontal));
            }

            return res;
        }
    }
}
