using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace View
{
    public partial class GameViewWinForm : Form, IGameView
    {
        Button[,] ButtonGrid;

        Button[,] CornerGrid;

        Button _lastSelectedWall;

        Dictionary<Button, (int, int)> _cornerDictionary;

        Dictionary<(int, int, bool), Button> _cornerToButtonDictionary;

        List<Button> _horizontalWalls;

        private Form _parentForm;

        public GameViewWinForm(Form parentForm)
        {
            InitializeComponent();

            _parentForm = parentForm;
        }

        public GameViewWinForm()
        {
            InitializeComponent();
        }

        public GameFieldState CurrentState { get; set; }

        public int SelectedCellX { get; set; }
        public int SelectedCellY { get; set; }
        public int SelectedCornerX { get; set; }
        public int SelectedCornerY { get; set; }

        public bool SelectedWallIsHorizontal { get; set; }

        public event Action PlacingTheWall;

        public event Action PlayerMove;
        public event Action ChangePlayer;
        public event Action DoUndo;

        public void DisplayPotentialWallsAndCorners(GameFieldState state)
        {
            _horizontalWalls = new List<Button>();

            _cornerDictionary = new Dictionary<Button, (int, int)>();

            _cornerToButtonDictionary = new Dictionary<(int, int, bool), Button>();

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

                    ButtonGrid[i, j].Height = (int) buttonSize;

                    ButtonGrid[i, j].Width = (int) buttonSize;

                    if (CurrentState.GridForColoring[i, j] == 1)
                    {
                        //ButtonGrid[i, j].Text = "PLAYER";
                        GraphicsPath p = new GraphicsPath();
                        p.AddEllipse(ButtonGrid[i, j].Location.X, ButtonGrid[i, j].Location.Y, buttonSize, buttonSize);
                        ButtonGrid[i, j].Region = new Region(p);

                        ButtonGrid[i, j].BackColor = Color.White;
                    }
                    
                    if (CurrentState.GridForColoring[i, j] == 2)
                    {
                        //ButtonGrid[i, j].Text = "PLAYER";
                        GraphicsPath p = new GraphicsPath();
                        p.AddEllipse(ButtonGrid[i, j].Location.X, ButtonGrid[i, j].Location.Y, buttonSize, buttonSize);
                        ButtonGrid[i, j].Region = new Region(p);

                        ButtonGrid[i, j].BackColor = Color.Black;
                    }

                    GamePanel.Controls.Add(ButtonGrid[i, j]);

                    ButtonGrid[i, j].Location = new Point(i * ((int) buttonSize + (int) wallSize),
                        j * ((int) buttonSize + (int) wallSize));

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

                    CornerGrid[i, j].Location = new Point(ButtonGrid[i, j].Location.X - wallSize,
                        ButtonGrid[i, j].Location.Y - wallSize);

                    CornerGrid[i, j].Click += SelectedCorner;

                    GamePanel.Controls.Add(CornerGrid[i, j]);
                }
            }

            for (int i = 0; i < state.Width; i++)
            {
                for (int j = 1; j < state.Height; j++)
                {
                    Button rightWall = new Button();

                    rightWall.Height = (2 * (int) buttonSize + (int) wallSize);

                    rightWall.Width = (int) wallSize;

                    rightWall.Location = new Point(ButtonGrid[i, j].Location.X - (int) wallSize,
                        ButtonGrid[i, j].Location.Y - (int) buttonSize - (int) wallSize);

                    _cornerDictionary.Add(rightWall, (i, j));

                    _cornerToButtonDictionary.Add((i, j, false), rightWall);

                    GamePanel.Controls.Add(rightWall);

                    rightWall.Click += Wall_Click;
                    ;
                }
            }

            for (int i = 0; i < state.Width - 1; i++)
            {
                for (int j = 1; j < state.Height; j++)
                {
                    Button higherWall = new Button();

                    higherWall.Width = (2 * (int) buttonSize + (int) wallSize);

                    higherWall.Height = (int) wallSize;

                    higherWall.Location = new Point(ButtonGrid[i, j].Location.X,
                        ButtonGrid[i, j].Location.Y - (int) wallSize);

                    _cornerDictionary.Add(higherWall, (i + 1, j));

                    _horizontalWalls.Add(higherWall);

                    _cornerToButtonDictionary.Add((i + 1, j, true), higherWall);

                    GamePanel.Controls.Add(higherWall);

                    higherWall.Click += Wall_Click;
                }
            }


            foreach (Button b in ButtonGrid)
            {
                b.Dispose();
            }

            foreach (Button b in CornerGrid)
            {
                b?.BringToFront();
            }


            foreach ((int id, int wC) in CurrentState._playersStates)
            {
                listBoxPlayers.Items.Add(id + "                  " + wC);
            }

            listBoxPlayers.Items.Clear();

            labelCurPlayer.Text = "Current Player ID:" + CurrentState.CurrentPlayerID;
        }

        private void SelectedCorner(object sender, EventArgs e)
        {
            Button selectedCorner = (Button) sender;

            int wallSize = GamePanel.Width / CurrentState.GridForPlayers.Length / 5;
            int buttonSize = GamePanel.Width / CurrentState.GridForPlayers.Length;
            int selectedCornerX = selectedCorner.Location.X + wallSize - buttonSize;
            int selectedCornerY = selectedCorner.Location.Y + wallSize - buttonSize;

            MessageBox.Show("SelectedCornerX: " + selectedCornerX + " SelectedCornerY: " + selectedCornerY);
        }

        private void Wall_Click(object sender, EventArgs e)
        {
            Button selectedWall = (Button) sender;

            (int SelectedX, int SelectedY) = _cornerDictionary[selectedWall];

            SelectedCornerX = SelectedX;

            SelectedCornerY = SelectedY;

            _lastSelectedWall = selectedWall;

            SelectedWallIsHorizontal = _horizontalWalls.Contains(selectedWall);

            PlacingTheWall?.Invoke();
        }

        public void PlaceTheWall(GameFieldState state)
        {
            _lastSelectedWall =
                _cornerToButtonDictionary[(state.SelectedCornerX, state.SelectedCornerY, state.TheWallIsHorisontal)];

            _lastSelectedWall.Enabled = false;

            _lastSelectedWall.BackColor = Color.Red;

            _lastSelectedWall.BringToFront();

            foreach (Button b in ButtonGrid)
            {
                b.Dispose();
            }

            listBoxPlayers.Items.Clear();
        }


        public void DisplayTheField(GameFieldState state)
        {
            CurrentState = state;

            ButtonGrid = new Button[CurrentState.Height, CurrentState.Width];

            GamePanel.Width = 4500;

            GamePanel.Height = GamePanel.Width;

            int buttonSize = GamePanel.Width / state.GridForPlayers.Length;

            int wallSize = buttonSize / 5;


            for (int i = 0; i < state.Width; i++)
            {
                for (int j = 0; j < state.Height; j++)
                {
                    ButtonGrid[i, j] = new Button();

                    ButtonGrid[i, j].Height = (int) buttonSize;

                    ButtonGrid[i, j].Width = (int) buttonSize;

                    if (CurrentState.GridForColoring[i, j] == 1)
                    {
                        GraphicsPath p = new GraphicsPath();
                        p.AddEllipse(ButtonGrid[i, j].Location.X, ButtonGrid[i, j].Location.Y, buttonSize, buttonSize);
                        ButtonGrid[i, j].Region = new Region(p);

                        ButtonGrid[i, j].BackColor = Color.White;
                    }

                    if (CurrentState.GridForColoring[i, j] == 2)
                    {
                        GraphicsPath p = new GraphicsPath();
                        p.AddEllipse(ButtonGrid[i, j].Location.X, ButtonGrid[i, j].Location.Y, buttonSize, buttonSize);
                        ButtonGrid[i, j].Region = new Region(p);

                        ButtonGrid[i, j].BackColor = Color.Black;
                    }
                    
                    GamePanel.Controls.Add(ButtonGrid[i, j]);

                    ButtonGrid[i, j].Location = new Point(i * ((int) buttonSize + (int) wallSize),
                        j * ((int) buttonSize + (int) wallSize));

                    ButtonGrid[i, j].Click += GameViewWinForm_Click;
                }
            }

            foreach (Button b in ButtonGrid)
            {
                b.BringToFront();
            }

            foreach ((int id, int wC) in CurrentState._playersStates)
            {
                listBoxPlayers.Items.Add(id + "                " + wC);
            }

            labelCurPlayer.Text = "Current Player ID:" + CurrentState.CurrentPlayerID;

            //listBoxPlayers.DataSource = CurrentState._playersStates;
        }

        private void GameViewWinForm_Click(object sender, EventArgs e)
        {
            Button selectedButton = (Button) sender;

            double wallSize = selectedButton.Width / 5;

            SelectedCellX = selectedButton.Location.X / (selectedButton.Width + (int) wallSize);

            SelectedCellY = selectedButton.Location.Y / (selectedButton.Width + (int) wallSize);

            foreach (Button b in ButtonGrid)
            {
                b.Dispose();
            }

            listBoxPlayers.Items.Clear();

            ChangeTheCell();

            //MessageBox.Show("Selected X: " + SelectedCellX + " Selected Y: " + SelectedCellY);
        }


        public void ChangeTheCell()
        {
            PlayerMove?.Invoke();
        }

        public void CantPlaceTheWall()
        {
            MessageBox.Show("Sorry, you can't place the wall this way.");
        }

        public void ThisIsTheEnd()
        {
            MessageBox.Show("The Game is Ended. Player number " + CurrentState.CurrentPlayerID +
                            " has won. CONGRATULATIONS!");

            Close();

            _parentForm.Show();

            Dispose();
        }
    }
}