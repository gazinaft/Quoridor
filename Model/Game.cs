using System.Collections.Generic;
using System.Linq;
using Model.Services;

namespace Model {
    public class Game {
        
        public GameField Board { get; set; }
        
        private IPlayer FirstPlayer;
        
        private IPlayer SecondPlayer;
        public Cell SelectedCell { get; set; }
        
        public Corner SelectedCorner { get; set; }
        
        public bool WallIsHorizintal { get; set; }

        private PathFindingService _pathFindingService;

        private MoveValidationService _moveValidationService;

        private WallValidationService _wallValidationService;
        
        public delegate void ChangeSelectedCell();

        public delegate void ChangeSelectedCorner();

        public event ChangeSelectedCell SelectedCellChanged;

        public event ChangeSelectedCorner NotifyPlacingTheWall;

        public delegate void NextPlayer();

        public delegate void CornerIsInvalid();

        public event CornerIsInvalid NotifyCornerIsInvalid;

        public event NextPlayer NotifyPlayerHasChanged;

        private List<IPlayer> Players;

        public IPlayer ActivePlayer;

        public Game()
        {
            Board = new GameField(9, 9);

            _pathFindingService = new PathFindingService();

            _moveValidationService = new MoveValidationService();

            _wallValidationService = new WallValidationService(_pathFindingService);
            
            Players = new List<IPlayer>();

            UserPlayer firstPlayer = new UserPlayer();

            firstPlayer.CurrentCell = Board.Cells[4, 8];

            firstPlayer.VictoryRow = 0;

            UserPlayer secondPlayer = new UserPlayer();

            secondPlayer.CurrentCell = Board.Cells[4, 0];

            secondPlayer.VictoryRow = 8;

            ActivePlayer = firstPlayer;

            firstPlayer.PlayerIsActive = true;

            Players.Add(firstPlayer);
            Players.Add(secondPlayer);

            //firstPLayer.CurrentCell.X = 5;

            this.NotifyPlayerHasChanged += FindNextPlayer;
            
            //player1 = new IPlayer();
            //player1.IsActive = true;
            
            //player2 = new IPlayer();
            //Players.Add();


        }

        public void PlaceTheWall() {

            if (_wallValidationService.CornerInvalid(SelectedCorner.X, SelectedCorner.Y, WallIsHorizintal, Board, ActivePlayer))
            {

                NotifyCornerIsInvalid?.Invoke();

            }

            else {

                Board.SetBlock(SelectedCorner.X, SelectedCorner.Y, WallIsHorizintal);

                NotifyPlacingTheWall?.Invoke();

                NotifyPlayerHasChanged?.Invoke();

            }

        }

        

        public void FindNextPlayer() {

            int _lastActivePlayerIndex = 0;

            _lastActivePlayerIndex = Players.FindLastIndex(pl => pl.PlayerIsActive);

            if (_lastActivePlayerIndex + 1 != Players.Count)
            {                

                Players.ElementAt(_lastActivePlayerIndex).PlayerIsActive = false;

                Players.ElementAt(_lastActivePlayerIndex + 1).PlayerIsActive = true;

                ActivePlayer = Players.ElementAt(_lastActivePlayerIndex + 1);

            }
            else
            {
                
                Players.ElementAt(_lastActivePlayerIndex).PlayerIsActive = false;

                Players.ElementAt(0).PlayerIsActive = true;

                ActivePlayer = Players.ElementAt(0);

            }

        }

        public void ChangeTheCell() {

            Board.MovePlayer(SelectedCell.X, SelectedCell.Y, ActivePlayer);

            NotifyPlayerHasChanged?.Invoke();
        
        }

    }
    
}
