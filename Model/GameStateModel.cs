using System.Collections.Generic;

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

        public ICommand ComToGet { get; set; }

        public GameStateModel(Game game)
        {
            HasToWin = game.SecondPlayer; 

            HasToLose = game.FirstPlayer;

            ActivePlayer = game.ActivePlayer;

            InActivePlayer = game.InActivePlayer;

            ActivePlayer.PlayerIsActive = true;

            Board = game.Board;

            Players = new List<IPlayer> { HasToLose, HasToWin };

        }

        public bool IsTerminal() {
            return HasToWin.IsVictory() || HasToLose.IsVictory();
        }
        
        private GameStateModel(GameStateModel other) {
            Board = new GameField(other.Board);
            HasToWin = other.HasToWin.InfoClone(Board);
            HasToLose = other.HasToLose.InfoClone(Board);
            InActivePlayer = HasToLose;
            ActivePlayer = HasToWin;
            Players = new List<IPlayer> { HasToLose, HasToWin };
        }

        private GameStateModel DeepTurn(ICommand com) {
            return com.Execute(new GameStateModel(this) {ComToGet = com});
        }

        public List<GameStateModel> GetChildren() {
            var res = new List<GameStateModel>();
            var coms = GetLegalActions();
            for (var i = 0; i < coms.Count; i++) {
                var com = coms[i];
                res.Add(DeepTurn(com));
            }

            return res;
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

        public void PlaceTheWall(int x, int y, bool isHorizontal, bool toAdd = true) {

            Board.SetBlock(x, y, isHorizontal, toAdd);

            ActivePlayer.WallsCounter +=  toAdd ? - 1 : 1;

            DefineNextPlayer();

        }

        public List<ICommand> GetLegalActions()
        {
            var res = new List<ICommand>();

            var list = Board.GetAvailableMoves(ActivePlayer);
            for (var i = 0; i < list.Count; i++)
            {
                res.Add(new MovePlayerCommand(list[i]));
            }

            foreach (var (c, isHorizontal) in Board.GetAvailableWalls(Players, InActivePlayer))
            {
                res.Add(new PlaceWallCommand(c.X, c.Y, isHorizontal));
            }

            return res;
        }
        
    }
}
