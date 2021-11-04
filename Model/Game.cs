using System.Collections.Generic;
using System.Linq;
using Model.Services;
using Model.Strategy;
namespace Model {
    public class Game {
        
        public GameField Board { get; set; }
        
        private IPlayer FirstPlayer;
        
        private IPlayer SecondPlayer;

        public ICommand LastCommand { get; set; }

        public bool TheWallIsPlaced { get; set; }
        
        public Cell SelectedCell { get; set; }
        
        public Corner SelectedCorner { get; set; }
        
        public bool WallIsHorizontal { get; set; }

        public bool IsJumping { get; set; }

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

        public List<IPlayer> Players;

        public IPlayer ActivePlayer;

        public LinkedList<ICommand> _stepsHistory;

        public bool DoDisplayStep { get; set; }

        public Game(IPlayerStrategy enemyStrategy) {

            DoDisplayStep = true;
            
            _stepsHistory = new LinkedList<ICommand>();
            
            _pathFindingService = new PathFindingService();

            _moveValidationService = new MoveValidationService();

            _wallValidationService = new WallValidationService(_pathFindingService);

            Board = new GameField(_moveValidationService, _wallValidationService, _pathFindingService, 9, 9);

            Players = new List<IPlayer>();

            UserPlayer firstPlayer = new UserPlayer();
            firstPlayer.StartCell = Board.Cells[4, 8];

            firstPlayer.PlayerId = 1;
            firstPlayer.CurrentCell = Board.Cells[4, 8];
            firstPlayer.VictoryRow = 0;
            
            UserPlayer secondPlayer = new UserPlayer();
            secondPlayer.PlayerStrategy = enemyStrategy;
            secondPlayer.StartCell = Board.Cells[4, 0];
            secondPlayer.CurrentCell = Board.Cells[4, 0];
            secondPlayer.VictoryRow = 8;
            secondPlayer.PlayerId = 2;

            ActivePlayer = firstPlayer;

            firstPlayer.PlayerIsActive = true;

            Players.Add(firstPlayer);
            Players.Add(secondPlayer);

            NotifyPlayerHasChanged += FindNextPlayer;
            
            NotifyNextStep += MakeNextStep;
        }

        public Game Undo(ICommand terminalCommand) {

            DoDisplayStep = false;
            
            Players.ForEach(p => p.CurrentCell = p.StartCell);
            Players.ForEach(p => p.WallsCounter=10);

            foreach (ICommand c in _stepsHistory) {

                if (c!=terminalCommand) {

                    c.Execute(this);
                
                }

            }

            DoDisplayStep = true;

            return this;
        
        }

        public Game()
        {
            DoDisplayStep = true;
            
            _stepsHistory = new LinkedList<ICommand>();

            _pathFindingService = new PathFindingService();
            _moveValidationService = new MoveValidationService();
            _wallValidationService = new WallValidationService(_pathFindingService);
            Board = new GameField(_moveValidationService, _wallValidationService, _pathFindingService, 9, 9);

            Players = new List<IPlayer>();

            UserPlayer firstPlayer = new UserPlayer();
            firstPlayer.StartCell = Board.Cells[4, 8];
            firstPlayer.CurrentCell = Board.Cells[4, 8];
            firstPlayer.VictoryRow = 0;

            UserPlayer secondPlayer = new UserPlayer();
            secondPlayer.CurrentCell = Board.Cells[4, 0];
            secondPlayer.StartCell = Board.Cells[4, 0];
            secondPlayer.VictoryRow = 8;

            ActivePlayer = firstPlayer;

            firstPlayer.PlayerIsActive = true;
            
            firstPlayer.PlayerId = 1;
            secondPlayer.PlayerId = 2;
            
            Players.Add(firstPlayer);
            Players.Add(secondPlayer);
            
            NotifyPlayerHasChanged += FindNextPlayer;
            NotifyNextStep += MakeNextStep;
            
        }

        public void MakeNextStep() {

            if (ActivePlayer.PlayerStrategy !=null) {

                ActivePlayer.Decide(this);
                NotifyBotHasDecided?.Invoke();
                NotifyPlayerHasChanged?.Invoke();
            
            }
        
        }

        public void PlaceTheWall() {

            if (_wallValidationService.CornerInvalid(SelectedCorner.X, SelectedCorner.Y, WallIsHorizontal, Board, Players))
            {
                NotifyCornerIsInvalid?.Invoke();
            }
            else {

                if (DoDisplayStep) {

                    PlaceWallCommand _lastCommand = new PlaceWallCommand(SelectedCorner.X, SelectedCorner.Y, WallIsHorizontal);

                    _stepsHistory.AddLast(_lastCommand);

                    LastCommand = _lastCommand;

                }
                
                Board.SetBlock(SelectedCorner.X, SelectedCorner.Y, WallIsHorizontal);
                
                ActivePlayer.WallsCounter--;

                TheWallIsPlaced = true;

                NotifyPlayerHasChanged?.Invoke();

                if (DoDisplayStep) {

                    NotifyPlacingTheWall?.Invoke();

                }

                

            }
        }

        public void FindNextPlayer() {

            if (ActivePlayer.CurrentCell.Y == ActivePlayer.VictoryRow)
            {
                NotifyAboutEnd?.Invoke();
                return;
            }

            int lastActivePlayerIndex = 0;

            lastActivePlayerIndex = Players.FindLastIndex(pl => pl.PlayerIsActive);

            if (lastActivePlayerIndex + 1 != Players.Count)
            {
                Players.ElementAt(lastActivePlayerIndex).PlayerIsActive = false;
                Players.ElementAt(lastActivePlayerIndex + 1).PlayerIsActive = true;
                ActivePlayer = Players.ElementAt(lastActivePlayerIndex + 1);
            }
            else
            {
                Players.ElementAt(lastActivePlayerIndex).PlayerIsActive = false;
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

        }

        public void ChangePlayers() {

            Players.Clear();
            UserPlayer firstPlayer = new UserPlayer();
            firstPlayer.PlayerId = 1;
            firstPlayer.StartCell = Board.Cells[4, 0];
            firstPlayer.CurrentCell = Board.Cells[4, 0];
            firstPlayer.VictoryRow = 8;

            UserPlayer secondPlayer = new UserPlayer();
            secondPlayer.PlayerId = 2;
            secondPlayer.PlayerStrategy = new DummyStrategy();
            secondPlayer.StartCell = Board.Cells[4, 8];
            secondPlayer.CurrentCell = Board.Cells[4, 8];
            secondPlayer.VictoryRow = 0;
            

            ActivePlayer = firstPlayer;

            firstPlayer.PlayerIsActive = true;

            Players.Add(firstPlayer);
            Players.Add(secondPlayer);

        }

        public void ChangeTheCell() {
            

            if (DoDisplayStep) {

                MovePlayerCommand lastCommand = new MovePlayerCommand(SelectedCell, ActivePlayer);

                _stepsHistory.AddLast(lastCommand);

                LastCommand = lastCommand;

            }

            if (System.Math.Abs(SelectedCell.X - ActivePlayer.CurrentCell.X) + System.Math.Abs(SelectedCell.Y - ActivePlayer.CurrentCell.Y) > 1)
            {

                IsJumping = true;

            }
            else
            {

                IsJumping = false;

            }

            Board.MovePlayer(SelectedCell.X, SelectedCell.Y, ActivePlayer);

            TheWallIsPlaced = false;

            NotifyPlayerHasChanged?.Invoke();
        }

    }
    
}
