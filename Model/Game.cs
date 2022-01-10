using System.Collections.Generic;
using System.Linq;
using Model.Services;

namespace Model {
    using System;

    public class Game {
        
        public GameField Board { get; set; }
        public ICommand PlaceTheWallCommand { get; set; }
        public ICommand MovePlayerCommand { get; set; }

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
       
        public event Action<int> NotifyAboutEnd;
        public event BotStep NotifyBotHasDecided;
        public event NextStep NotifyNextStep;
        public event CornerIsInvalid NotifyCornerIsInvalid;
        public event NextPlayer NotifyPlayerHasChanged;

        public List<IPlayer> Players;
        public IPlayer ActivePlayer;
        public IPlayer InActivePlayer;
        public IPlayer FirstPlayer { get; }
        public IPlayer SecondPlayer { get; }
        public LinkedList<ICommand> _stepsHistory;

        public bool DoDisplayStep { get; set; }

        public GameStateModel GetGameState() {
            GameStateModel currentState = new GameStateModel(this) { Players = this.Players };
            return currentState;
        }

        public Game(IPlayerStrategy enemyStrategy) {

            DoDisplayStep = true;
            
            _stepsHistory = new LinkedList<ICommand>();
            _pathFindingService = new PathFindingService();
            _moveValidationService = new MoveValidationService();
            _wallValidationService = new WallValidationService(_pathFindingService);

            Board = new GameField(_moveValidationService, _wallValidationService, 9, 9);

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

            FirstPlayer = firstPlayer;
            SecondPlayer = secondPlayer;

            ActivePlayer = firstPlayer;
            InActivePlayer = secondPlayer;

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
            Board = new GameField(_moveValidationService, _wallValidationService, 9, 9);

            Players = new List<IPlayer>();

            UserPlayer firstPlayer = new UserPlayer();
            firstPlayer.StartCell = Board.Cells[4, 8];
            firstPlayer.CurrentCell = Board.Cells[4, 8];
            firstPlayer.VictoryRow = 0;

            FirstPlayer = firstPlayer;
            
            UserPlayer secondPlayer = new UserPlayer();
            secondPlayer.CurrentCell = Board.Cells[4, 0];
            secondPlayer.StartCell = Board.Cells[4, 0];
            secondPlayer.VictoryRow = 8;

            SecondPlayer = secondPlayer;
            ActivePlayer = firstPlayer;
            InActivePlayer = secondPlayer;

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

            if (_wallValidationService.CornerInvalid(SelectedCorner.X, SelectedCorner.Y, WallIsHorizontal, Board, Players)) {
                NotifyCornerIsInvalid?.Invoke();
            }
            else {
                Board.SetBlock(SelectedCorner.X, SelectedCorner.Y, WallIsHorizontal);
                ActivePlayer.WallsCounter--;
                TheWallIsPlaced = true;
                NotifyPlayerHasChanged?.Invoke();
                NotifyPlacingTheWall?.Invoke();
            }
        }

        public void DefineNextPlayer() {
            if (FirstPlayer.PlayerIsActive) {
                FirstPlayer.PlayerIsActive = false;
                SecondPlayer.PlayerIsActive = true;
                ActivePlayer = SecondPlayer;
                InActivePlayer = FirstPlayer;
            }
            else {
                SecondPlayer.PlayerIsActive = false;
                FirstPlayer.PlayerIsActive = true;
                InActivePlayer = SecondPlayer;
                ActivePlayer = FirstPlayer;
            }
        }

        public void FindNextPlayer() {
            if (ActivePlayer.CurrentCell.Y == ActivePlayer.VictoryRow) {
                SecondPlayer?.PlayerStrategy.SendVictory(this);
                NotifyAboutEnd?.Invoke(ActivePlayer.PlayerId);
                return;
            }

            int lastActivePlayerIndex = Players.FindLastIndex(pl => pl.PlayerIsActive);
            if (lastActivePlayerIndex + 1 != Players.Count) {
                InActivePlayer = Players.ElementAt(lastActivePlayerIndex);
                Players.ElementAt(lastActivePlayerIndex).PlayerIsActive = false;
                Players.ElementAt(lastActivePlayerIndex + 1).PlayerIsActive = true;
                ActivePlayer = Players.ElementAt(lastActivePlayerIndex + 1);
            }
            else {
                InActivePlayer = Players.ElementAt(lastActivePlayerIndex);
                Players.ElementAt(lastActivePlayerIndex).PlayerIsActive = false;
                Players.ElementAt(0).PlayerIsActive = true;
                ActivePlayer = Players.ElementAt(0);
            }

            if (ActivePlayer.PlayerStrategy != null) {
                ActivePlayer.Decide(this);               
                ActivePlayer.PlayerIsActive = false;
                InActivePlayer = ActivePlayer;
                ActivePlayer = Players.ElementAt(0);
                ActivePlayer.PlayerIsActive = true;
            }
        }

        public void ChangePlayers() {

            FirstPlayer.StartCell = Board.Cells[4, 0];
            FirstPlayer.CurrentCell = Board.Cells[4, 0];

            FirstPlayer.VictoryRow = 8;
            SecondPlayer.StartCell = Board.Cells[4, 8];
            SecondPlayer.CurrentCell = Board.Cells[4, 8];
            SecondPlayer.VictoryRow = 0;

            Players.Clear();
            Players.Add(FirstPlayer);
            Players.Add(SecondPlayer);

            ActivePlayer = SecondPlayer;
            SecondPlayer.PlayerIsActive = true;
            InActivePlayer = FirstPlayer;
            FirstPlayer.PlayerIsActive = false;
        }

        public void ChangeTheCell() {
            // if travel more than 1 cell than it's a jump
            IsJumping = System.Math.Abs(SelectedCell.X - ActivePlayer.CurrentCell.X) +
                System.Math.Abs(SelectedCell.Y - ActivePlayer.CurrentCell.Y) > 1;

            Board.MovePlayer(SelectedCell.X, SelectedCell.Y, ActivePlayer);

            TheWallIsPlaced = false;
            NotifyPlayerHasChanged?.Invoke();
        }

    }
    
}
