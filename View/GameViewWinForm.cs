using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace View
{
    public partial class GameViewWinForm : Form, IGameView
    {
        Button[,] ButtonGrid;

        Button[,] CornerGrid;

        Dictionary<Button, Button> _cornerDictionary;

        public GameViewWinForm()
        {
            InitializeComponent();
        }

        public GameViewWinForm(GameFieldState gameFieldState)
        {
            InitializeComponent();

            CurrentState = gameFieldState;

            DisplayPotentialWallsAndCorners(CurrentState);

            ButtonGrid = new Button[CurrentState.Height, CurrentState.Width];

            CornerGrid = new Button[CurrentState.GridForCorners.GetLength(0), CurrentState.GridForCorners.GetLength(0)];
        }

        public GameFieldState CurrentState { get; set; }

        public int SelectedCellX { get ; set; }
        public int SelectedCellY { get; set; }
        public int SelectedCornerX { get; set; }
        public int SelectedCornerY { get; set; }

        public event Action PlacingTheWall;
        
        public event Action PlayerMove;

        public void DisplayPotentialWallsAndCorners(GameFieldState state) {

            _cornerDictionary = new Dictionary<Button, Button>();
            
            CurrentState = state;

            ButtonGrid = new Button[CurrentState.Height, CurrentState.Width];

            CornerGrid = new Button[CurrentState.GridForCorners.GetLength(0), CurrentState.GridForCorners.GetLength(0)];

            GamePanel.Width = 4500;

            GamePanel.Height = GamePanel.Width;

            int buttonSize = GamePanel.Width / state.GridForPlayers.Length;

            int wallSize = buttonSize / 5;

            for (int i = 0; i < state.Width; i++)
            {

                for (int j = 0; j < state.Height; j++)
                {

                    ButtonGrid[i, j] = new Button();

                    ButtonGrid[i, j].Height = (int)buttonSize;

                    ButtonGrid[i, j].Width = (int)buttonSize;

                    if (CurrentState.GridForPlayers[i, j])
                    {

                        //ButtonGrid[i, j].Text = "PLAYER";

                        ButtonGrid[i, j].Image = Image.FromFile("C:\\Users\\Robert\\source\\repos\\Quoridor\\pictures\\fishka.jpg");

                    }

                    GamePanel.Controls.Add(ButtonGrid[i, j]);

                    ButtonGrid[i, j].Location = new Point(i * ((int)buttonSize + (int)wallSize), j * ((int)buttonSize + (int)wallSize));

                    ButtonGrid[i, j].Click += GameViewWinForm_Click;

                }

            }

            for (int i = 0; i < CornerGrid.GetLength(0) - 1; i++)
            {

                for (int j = 0; j < CornerGrid.GetLength(0) - 1; j++)
                {

                    CornerGrid[i, j] = new Button();

                    CornerGrid[i, j].Width = wallSize;

                    CornerGrid[i, j].Height = wallSize;

                    CornerGrid[i, j].BackColor = Color.IndianRed;

                    CornerGrid[i, j].Location = new Point(ButtonGrid[i, j].Location.X - wallSize, ButtonGrid[i, j].Location.Y - wallSize);

                    CornerGrid[i, j].Click += SelectedCorner;

                    GamePanel.Controls.Add(CornerGrid[i, j]);

                }

            }

            for (int i = 0; i < state.Width; i++)
            {
                for (int j = 1; j < state.Height; j++)
                {

                    Button rightWall = new Button();

                    rightWall.Height = (2 * (int)buttonSize + (int)wallSize);

                    rightWall.Width = (int)wallSize;

                    rightWall.Location = new Point(ButtonGrid[i, j].Location.X - (int)wallSize, ButtonGrid[i, j].Location.Y - (int)buttonSize - (int)wallSize);

                    _cornerDictionary.Add(rightWall, CornerGrid[i, j]);
                    
                    GamePanel.Controls.Add(rightWall);

                    rightWall.Click += Wall_Click; ;

                }

            }

            for (int i = 0; i < state.Width - 1; i++)
            {
                for (int j = 1; j < state.Height; j++)
                {
                    Button higherWall = new Button();

                    higherWall.Width = (2 * (int)buttonSize + (int)wallSize);

                    higherWall.Height = (int)wallSize;

                    higherWall.Location = new Point(ButtonGrid[i, j].Location.X, ButtonGrid[i, j].Location.Y - (int)wallSize);

                    _cornerDictionary.Add(higherWall, CornerGrid[i+1, j]);

                    GamePanel.Controls.Add(higherWall);

                    higherWall.Click += Wall_Click;

                }

            }

            

            foreach (Button b in ButtonGrid) {

                b.Dispose();
            
            }

            foreach (Button b in CornerGrid) {

                if (b != null) {

                    b.BringToFront();
                
                }
            
            }

        }

        private void SelectedCorner(object sender, EventArgs e)
        {
            Button selectedCorner = (Button)sender;

            int wallSize = GamePanel.Width / CurrentState.GridForPlayers.Length / 5;

            int buttonSize = GamePanel.Width / CurrentState.GridForPlayers.Length;

            int selectedCornerX = selectedCorner.Location.X + wallSize - buttonSize;

            int selectedCornerY = selectedCorner.Location.Y + wallSize - buttonSize;

            MessageBox.Show("SelectedCornerX: " + selectedCornerX + " SelectedCornerY: " + selectedCornerY);
        
        }

        private void Wall_Click(object sender, EventArgs e)
        {
            Button selectedWall = (Button)sender;

            Button selectedCorner = _cornerDictionary[selectedWall];

            int wallSize = GamePanel.Width / CurrentState.GridForPlayers.Length / 5;

            int buttonSize = GamePanel.Width / CurrentState.GridForPlayers.Length;

            int selectedCornerX = selectedCorner.Location.X + wallSize - buttonSize;

            int selectedCornerY = selectedCorner.Location.Y + wallSize - buttonSize;

            MessageBox.Show("SelectedCornerX: " + selectedCornerX + " SelectedCornerY: " + selectedCornerY);
        }


        public void DisplayTheField(GameFieldState state)
        {
            CurrentState = state;

            ButtonGrid = new Button[CurrentState.Height, CurrentState.Width];

            GamePanel.Width = 4500;
            
            GamePanel.Height = GamePanel.Width;

            int buttonSize = GamePanel.Width / state.GridForPlayers.Length;

            int wallSize = buttonSize / 5;


            for (int i = 0; i < state.Width; i++ ) {

                for (int j =0; j < state.Height; j++) {

                    ButtonGrid[i, j] = new Button();

                    ButtonGrid[i, j].Height = (int)buttonSize;

                    ButtonGrid[i, j].Width = (int)buttonSize;

                    if (CurrentState.GridForPlayers[i, j]) {

                        //ButtonGrid[i, j].Text = "PLAYER";

                        ButtonGrid[i, j].Image = Image.FromFile("C:\\Users\\Robert\\source\\repos\\Quoridor\\pictures\\fishka.jpg");

                    }

                    GamePanel.Controls.Add(ButtonGrid[i, j]);

                    ButtonGrid[i, j].Location = new Point( i * ((int)buttonSize + (int)wallSize), j* ((int)buttonSize + (int)wallSize));

                    ButtonGrid[i, j].Click += GameViewWinForm_Click;

                }

            }

        }

        private void GameViewWinForm_Click(object sender, EventArgs e)
        {
            Button selectedButton = (Button)sender;

            double wallSize = selectedButton.Width / 5;

            SelectedCellX = selectedButton.Location.X / (selectedButton.Width + (int) wallSize);

            SelectedCellY = selectedButton.Location.Y / (selectedButton.Width + (int)wallSize);

            foreach (Button b in ButtonGrid) {

                b.Dispose();
            
            }

            ChangeTheCell();

            //MessageBox.Show("Selected X: " + SelectedCellX + " Selected Y: " + SelectedCellY);

        }

        public void ChangeTheCell() {

            PlayerMove.Invoke();

        }
    }
}
