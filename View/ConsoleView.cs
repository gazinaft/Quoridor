using System;
using System.Collections.Generic;

namespace View
{
    public class ConsoleView : IGameView
    {
        Dictionary<string, int> _lettersToCellCoordinates;
        Dictionary<string, int> _lettersToCornerCorodinates;
        Dictionary<int, string> _coordinatesCornersToLetters;
        Dictionary<int, string> _coordinatesCellsToLetters;

        public int SelectedCellX { get ; set; }
        public int SelectedCellY { get; set; }
        public int SelectedCornerX { get; set; }
        public int SelectedCornerY { get; set; }
        public bool SelectedWallIsHorizontal { get; set; }

        public event Action PlacingTheWall;
        public event Action PlayerMove;
        public event Action ChangePlayer;
        public event Action DoUndo;

        public ConsoleView()
        {
            _lettersToCellCoordinates = new Dictionary<string, int>();

            _lettersToCornerCorodinates = new Dictionary<string, int>();

            _coordinatesCornersToLetters = new Dictionary<int, string>();

            _coordinatesCellsToLetters = new Dictionary<int, string>();
            
            _lettersToCellCoordinates.Add("a", 0);
            _lettersToCellCoordinates.Add("A", 0);
            _lettersToCellCoordinates.Add("b", 1);
            _lettersToCellCoordinates.Add("B", 1);
            _lettersToCellCoordinates.Add("c", 2);
            _lettersToCellCoordinates.Add("C", 2);
            _lettersToCellCoordinates.Add("D", 3);
            _lettersToCellCoordinates.Add("d", 3);
            _lettersToCellCoordinates.Add("E", 4);
            _lettersToCellCoordinates.Add("e", 4);
            _lettersToCellCoordinates.Add("F", 5);
            _lettersToCellCoordinates.Add("f", 5);
            _lettersToCellCoordinates.Add("G", 6);
            _lettersToCellCoordinates.Add("g", 6);
            _lettersToCellCoordinates.Add("h", 7);
            _lettersToCellCoordinates.Add("H", 7);
            _lettersToCellCoordinates.Add("i", 8);
            _lettersToCellCoordinates.Add("I", 8);

            _lettersToCornerCorodinates.Add("s", 1);
            _lettersToCornerCorodinates.Add("S", 1);
            _lettersToCornerCorodinates.Add("T", 2);
            _lettersToCornerCorodinates.Add("t", 2);
            _lettersToCornerCorodinates.Add("u", 3);
            _lettersToCornerCorodinates.Add("U", 3);
            _lettersToCornerCorodinates.Add("v", 4);
            _lettersToCornerCorodinates.Add("V", 4);
            _lettersToCornerCorodinates.Add("W", 5);
            _lettersToCornerCorodinates.Add("w", 5);
            _lettersToCornerCorodinates.Add("x", 6);
            _lettersToCornerCorodinates.Add("X", 6);
            _lettersToCornerCorodinates.Add("y", 7);
            _lettersToCornerCorodinates.Add("Y", 7);
            _lettersToCornerCorodinates.Add("z", 8);
            _lettersToCornerCorodinates.Add("Z", 8);

            _coordinatesCornersToLetters.Add(1, "S");
            _coordinatesCornersToLetters.Add(2, "T");
            _coordinatesCornersToLetters.Add(3, "U");
            _coordinatesCornersToLetters.Add(4, "V");
            _coordinatesCornersToLetters.Add(5, "W");
            _coordinatesCornersToLetters.Add(6, "X");
            _coordinatesCornersToLetters.Add(7, "Y");
            _coordinatesCornersToLetters.Add(8, "Z");

            _coordinatesCellsToLetters.Add(0, "A");
            _coordinatesCellsToLetters.Add(1, "B");
            _coordinatesCellsToLetters.Add(2, "C");
            _coordinatesCellsToLetters.Add(3, "D");
            _coordinatesCellsToLetters.Add(4, "E");
            _coordinatesCellsToLetters.Add(5, "F");
            _coordinatesCellsToLetters.Add(6, "G");
            _coordinatesCellsToLetters.Add(7, "H");
            _coordinatesCellsToLetters.Add(8, "I");



        }

        public void CantPlaceTheWall()
        {
            //Console.WriteLine("You can't place the wall in that way.");
        }

        public void DisplayPotentialWallsAndCorners(GameFieldState state)
        {
            /*Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("+++A+++++B+++++C+++++D+++++E+++++F+++++G+++++H+++++I+++");
            
            for (int i = 0; i < state.GridForPlayers.GetLength(0); i++)
            {

                string one = "■■■■■";

                int rowCounter = i + 1;

                string rowCounterString = rowCounter.ToString();
                
                for (int u = 0; u < 3; u++) {

                    for (int j = 0; j < state.GridForPlayers.GetLength(0); j++)
                    {

                        if (state.GridForPlayers[j, i])
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;

                            Console.Write("+");

                            Console.ForegroundColor = ConsoleColor.DarkRed;

                            Console.Write(one);

                        }
                        else
                        {

                            Console.ForegroundColor = ConsoleColor.Yellow;

                            Console.Write("+" + one);

                        }

                        if (j == 8)
                        {
                            Console.Write("+");
                            Console.Write("\r\n");

                        }

                    }


                }

                if (i == 8)
                {
                    
                    Console.WriteLine("++++++S+++++T+++++U+++++V+++++W+++++X+++++Y+++++Z++++++");
                
                }
                else {
                    
                    Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++");

                }
                
                

            }*/

            //Console.WriteLine("Choose white or black");

            CheckTheFirstCommand();

            //CheckTheCommand();
        }

        public void TryToMovePlayer() {


            PlayerMove?.Invoke();

        
        }

        public void CheckTheFirstCommand() {

            string selectedTeam = Console.ReadLine();

            if (selectedTeam == "white")
            {

                //CheckTheCommand();

                SelectedCellX = 4;

                SelectedCellY = 8;

                TryToMovePlayer();

            }
            else if (selectedTeam == "black")
            {

                ChangePlayer?.Invoke();

                SelectedCellX = 4;

                SelectedCellY = 0;

                TryToMovePlayer();

            }
            else {

                CheckTheFirstCommand();
            
            }
        
        
        }

        public void CheckTheCommand() {

            string command = Console.ReadLine();

            if (command.StartsWith("wall"))
            {
                string selectedCoordinates = command.Substring(5);

                string XString = selectedCoordinates.Substring(0, 1);

                string YString = selectedCoordinates.Substring(1, 1);

                string isHorisontal = selectedCoordinates.Substring(2);

                if (isHorisontal == "h" || isHorisontal == "H")
                {

                    SelectedWallIsHorizontal = true;


                }
                else if (isHorisontal == "v" || isHorisontal == "V") {

                    SelectedWallIsHorizontal = false;
                
                }

                try
                {
                    SelectedCornerX = _lettersToCornerCorodinates[XString];

                    SelectedCornerY = Convert.ToInt32(YString);

                    PlacingTheWall?.Invoke();

                }
                catch (Exception) {

                    Console.WriteLine("Can't parse the command:(((");

                }


            }
            else if (command.StartsWith("jump"))
            {
                string selectedCoordinates = command.Substring(5);

                string XString = selectedCoordinates.Substring(0);

                string YString = selectedCoordinates.Substring(1);

                try
                {

                    SelectedCellX = _lettersToCellCoordinates[XString];

                    SelectedCellY = Convert.ToInt32(YString) - 1;

                    TryToMovePlayer();

                }
                catch (Exception)
                {

                    Console.WriteLine("Can't parse the command:(((");

                }


            }
            else if (command.StartsWith("move"))
            {
                string selectedCoordinates = command.Substring(5);

                string XString = selectedCoordinates.Substring(0, 1);

                string YString = selectedCoordinates.Substring(1);

                try
                {

                    SelectedCellX = _lettersToCellCoordinates[XString];

                    SelectedCellY = Convert.ToInt32(YString) - 1;

                    TryToMovePlayer();

                }
                catch (Exception){

                    Console.WriteLine("Can't parse the command:((");

                }



            }
            else {

                Console.WriteLine("Sorry, I can't recognise the command...");

                //DisplayTheField();
            
            }
        
        }

        public void DisplayTheField(GameFieldState state)
        {

            /*Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("+++A+++++B+++++C+++++D+++++E+++++F+++++G+++++H+++++I+++");

            for (int i = 0; i < state.GridForPlayers.GetLength(0); i++)
            {

                string one = "■■■■■";

                int rowCounter = i + 1;

                string rowCounterString = rowCounter.ToString();

                for (int u = 0; u < 3; u++)
                {

                    for (int j = 0; j < state.GridForPlayers.GetLength(0); j++)
                    {

                        if (state.GridForPlayers[j, i])
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;

                            Console.Write("+");

                            Console.ForegroundColor = ConsoleColor.DarkRed;

                            Console.Write(one);

                        }
                        else
                        {

                            Console.ForegroundColor = ConsoleColor.Yellow;

                            Console.Write("+" + one);

                        }

                        if (j == 8)
                        {
                            Console.Write("+");
                            Console.Write("\r\n");

                        }

                    }


                }

                if (i == 8)
                {

                    Console.WriteLine("++++++S+++++T+++++U+++++V+++++W+++++X+++++Y+++++Z++++++");

                }
                else
                {

                    Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++");

                }



            }*/

            if (state.TheWallIsPlaced) {

                if (state.TheWallIsHorisontal) {

                    int selectedCornerY = state.SelectedCornerY;

                    Console.WriteLine("wall " + _coordinatesCornersToLetters[state.SelectedCornerX] + selectedCornerY + "h");

                }
                else {

                    int selectedCornerY = state.SelectedCornerY;

                    Console.WriteLine("wall " + _coordinatesCornersToLetters[state.SelectedCornerX] +  selectedCornerY + "h");
                
                }
            
            }
            else {

                if (state.IsJumping)
                {

                    int selectedCellY = state.SelectedCellY + 1;

                    Console.WriteLine("jump " + _coordinatesCellsToLetters[state.SelectedCellX] + selectedCellY);

                }
                else {

                    int selectedCellY = state.SelectedCellY + 1;

                    Console.WriteLine("move " + _coordinatesCellsToLetters[state.SelectedCellX] + selectedCellY);

                }
            
            }

            CheckTheCommand();
        }

        public void PlaceTheWall()
        {
            //Console.WriteLine("The wall was placed successfully.");
        }

        public void ThisIsTheEnd()
        {
            Console.WriteLine("The Game is ended.");
        }
    }
}
