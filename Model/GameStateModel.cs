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

        public IPlayer ActivePlayer { get; set; }

        public IPlayer InActivePlayer { get; set; }

        public GameStateModel(Game game)
        {
            HasToWin = game.FirstPlayer; 

            HasToLose = game.SecondPlayer;

            ActivePlayer = game.ActivePlayer;

            InActivePlayer = game.InActivePlayer;

            ActivePlayer.PlayerIsActive = true;

            Board = game.Board;

            Players = new List<IPlayer>();

            Players.Add(HasToWin);

            Players.Add(HasToWin);

        }

        public void DefineNextPlayer() {

            if (HasToWin.PlayerIsActive)
            {

                HasToWin.PlayerIsActive = false;

                HasToLose.PlayerIsActive = true;

                InActivePlayer = ActivePlayer;

                ActivePlayer = HasToLose;

            }
            else {

                HasToWin.PlayerIsActive = true;

                HasToLose.PlayerIsActive = false;

                InActivePlayer = ActivePlayer;

                ActivePlayer = HasToWin;

            }
        
        }

        public void MakeMove(int x, int y) {

            Board.MovePlayer(x, y, ActivePlayer);

            DefineNextPlayer();
        
        }

        public void PlaceTheWall(int x, int y, bool isHorizontal, bool ToAdd = true) {

            Board.SetBlock(x, y, isHorizontal, ToAdd);

            ActivePlayer.WallsCounter +=  ToAdd ? - 1 : 1;

            DefineNextPlayer();

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
