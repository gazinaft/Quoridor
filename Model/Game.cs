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

        private List<IPlayer> Players;

        public IPlayer ActivePlayer;

        public Game()
        {
            Board = new GameField(9, 9);
            Players = new List<IPlayer>();
            
            //player1 = new IPlayer();
            //player1.IsActive = true;
            
            //player2 = new IPlayer();
            //Players.Add();


        }

        public void GameLoop() {

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

        }
    }
    
}
