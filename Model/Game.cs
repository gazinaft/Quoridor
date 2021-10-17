using System.Collections.Generic;
using System.Linq;
using Model.Services;
using Model.Strategy;
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

        public delegate void NextStep();

        public delegate void BotStep();

        public delegate void TheGameIsEnded();

        public event TheGameIsEnded NotifyAboutEnd;

        public event BotStep NotifyBotHasDecided;

        public event NextStep NotifyNextStep;

        public event CornerIsInvalid NotifyCornerIsInvalid;

        public event NextPlayer NotifyPlayerHasChanged;

        private List<IPlayer> Players;

        public IPlayer ActivePlayer;

        public Game(IPlayerStrategy enemyStrategy) {

            _pathFindingService = new PathFindingService();

            _moveValidationService = new MoveValidationService();

            _wallValidationService = new WallValidationService(_pathFindingService);

            Board = new GameField(_moveValidationService, _wallValidationService, _pathFindingService, 9, 9);

            Players = new List<IPlayer>();

            UserPlayer firstPlayer = new UserPlayer();

            firstPlayer.CurrentCell = Board.Cells[4, 8];

            firstPlayer.VictoryRow = 0;

            UserPlayer secondPlayer = new UserPlayer();

            secondPlayer.PlayerStrategy = enemyStrategy;

            secondPlayer.CurrentCell = Board.Cells[4, 0];

            secondPlayer.VictoryRow = 8;

            ActivePlayer = firstPlayer;

            firstPlayer.PlayerIsActive = true;

            Players.Add(firstPlayer);
            Players.Add(secondPlayer);

            //firstPLayer.CurrentCell.X = 5;

            this.NotifyPlayerHasChanged += FindNextPlayer;

            this.NotifyNextStep += MakeNextStep;
        }
        
        public Game()
        {
            _pathFindingService = new PathFindingService();

            _moveValidationService = new MoveValidationService();

            _wallValidationService = new WallValidationService(_pathFindingService);

            Board = new GameField(_moveValidationService, _wallValidationService, _pathFindingService, 9, 9);

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

            this.NotifyNextStep += MakeNextStep;
            
            //player1 = new IPlayer();
            //player1.IsActive = true;
            
            //player2 = new IPlayer();
            //Players.Add();


        }

        public void MakeNextStep() {

            if (ActivePlayer.PlayerStrategy !=null) {

                ActivePlayer.Decide(this);
                NotifyBotHasDecided?.Invoke();
                NotifyPlayerHasChanged?.Invoke();
            
            }
        
        }

        public void PlaceTheWall() {

            if (_wallValidationService.CornerInvalid(SelectedCorner.X, SelectedCorner.Y, WallIsHorizintal, Board, Players))
            {

                NotifyCornerIsInvalid?.Invoke();

            }

            else {

                Board.SetBlock(SelectedCorner.X, SelectedCorner.Y, WallIsHorizintal);

                ActivePlayer.WallsCounter--;

                NotifyPlayerHasChanged?.Invoke();

                NotifyPlacingTheWall?.Invoke();

            }

        }

        public void FindNextPlayer() {

            if (ActivePlayer.CurrentCell.Y == ActivePlayer.VictoryRow)
            {

                NotifyAboutEnd?.Invoke();
                return;

            }

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

            if (ActivePlayer.PlayerStrategy != null)
            {

                ActivePlayer.Decide(this);

                ActivePlayer.PlayerIsActive = false;

                ActivePlayer = Players.ElementAt(0);

                ActivePlayer.PlayerIsActive = true;

            }

            //NotifyNextStep?.Invoke();

        }

        public void ChangeTheCell() {

            Board.MovePlayer(SelectedCell.X, SelectedCell.Y, ActivePlayer);

            NotifyPlayerHasChanged?.Invoke();
        
        }

    }
    
}
