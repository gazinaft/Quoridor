using System;

namespace View
{
    public class ConsoleView : IGameView
    {
        public int SelectedCellX { get ; set; }
        public int SelectedCellY { get; set; }
        public int SelectedCornerX { get; set; }
        public int SelectedCornerY { get; set; }

        public event Action PlacingTheWall;
        public event Action PlayerMove;

        public void DisplayPotentialWallsAndCorners(GameFieldState state)
        {
            throw new NotImplementedException();
        }

        public void DisplayTheBoard(int firstPlayerX, int firstPlayerY, int secondPlayerX, int secondPlayerY) {

            
            
            for (int i =0; i <=8; i++) {

                Console.WriteLine("#----#----#----#----#----#----#----#----#----#");
                for (int j=0; j <=8; j++) {

                    string curRow1 = "#----#----#----#----#----#----#----#----#----#";


                    if (i == firstPlayerX & j== firstPlayerY) { 
                    

                    
                    }
                
                }
                

            }


        }

        public void DisplayTheField(GameFieldState state)
        {
            throw new NotImplementedException();
        }

        public void GameLoop() { }
    }
}
