using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model {
    public class Game {
        
        private GameField Board;
        private IPlayer FirstPlayer;
        private IPlayer SecondPlayer;
        private Cell SelectedCell;
        private Corner SelectedCorner;
        private bool WallIsHorizintal;

        public delegate void ChangeSelectedCell(Cell cell, IPlayer player);

        public delegate void ChangeSelectedCorner(Cell firstCell, Cell secondCell, IPlayer player);

        public event ChangeSelectedCell SelectedCellChanged;

        public event ChangeSelectedCorner NotifyPlacingTheWall;

        public delegate void NextPlayer();

        public event NextPlayer NotifyPlayerHasChanged;

        private List<IPlayer> Players;

        public IPlayer ActivePlayer;

        public Game()
        {
            Board = new GameField(9, 9);
            Players = new List<IPlayer>();

            this.NotifyPlayerHasChanged += FindNextPlayer;
            
            //player1 = new IPlayer();
            //player1.IsActive = true;
            
            //player2 = new IPlayer();
            //Players.Add();


        }

        public void PlaceTheWall(IPlayer player) {

            Board.SetBlock(SelectedCorner.X, SelectedCorner.Y, WallIsHorizintal);

            NotifyPlayerHasChanged?.Invoke();

        }

        

        public void FindNextPlayer() {

            if (Players.ElementAt(Players.FindIndex(pl => pl.PlayerIsActive && pl != null) + 1) != null)
            {

                Players.ElementAt(Players.FindIndex(pl => pl.PlayerIsActive && pl != null) + 1).PlayerIsActive = true;

                ActivePlayer = Players.ElementAt(Players.FindIndex(pl => pl.PlayerIsActive && pl != null) + 1);

            }
            else
            {

                Players.ElementAt(0).PlayerIsActive = true;

                ActivePlayer = Players.ElementAt(0);

            }

        }

        public void ChangeTheCell() {

            Board.MovePlayer(SelectedCell.X, SelectedCell.Y, ActivePlayer);

            NotifyPlayerHasChanged?.Invoke();
        
        }

        

        /*public void GameLoop() {

            while (!(Players.Exists(player => player.VictoryRow == player.CurrentCell.Y))) {

                foreach (IPlayer currentPlayer in Players) {

                    if (currentPlayer.PlayerIsActive) {

                        //if (currentPlayer.State == PlayerState.ChangeTheCell)
                        //{

                          //  Board.MovePlayer(SelectedCell, currentPlayer);

                        //}
                        //else if (currentPlayer.State == PlayerState.PlaceTheWall)
                        //{

                          //  Board.SetBlock(SelectedCorner.X, SelectedCorner.Y, WallIsHorizintal);

                        //}


                        if (Players.ElementAt(Players.FindIndex(pl => pl.PlayerIsActive && pl != null) + 1) != null)
                        {

                            Players.ElementAt(Players.FindIndex(pl => pl.PlayerIsActive && pl != null) + 1).PlayerIsActive = true;

                            currentPlayer.PlayerIsActive = false;

                        }
                        else {

                            Players.ElementAt(0).PlayerIsActive = true;
                            currentPlayer.PlayerIsActive = false;

                        }

                    }                    

                }

            }

        }*/
    }
    
}
