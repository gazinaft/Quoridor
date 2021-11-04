﻿namespace Model {
    public class PlaceWallCommand: ICommand {

        private readonly int _x;
        private readonly int _y;
        private readonly bool _direction;

        
        public PlaceWallCommand(int x, int y, bool isHorizontal) {
            _x = x;
            _y = y;
            _direction = isHorizontal;
        }
        
        public Game Execute(Game game) {
            game.SelectedCorner = game.Board.Corners[_x, _y];
            game.WallIsHorizontal = _direction;
            game.PlaceTheWall();
            return game;
        }

        public void Undo(Game game) {
            game.Undo(this);
        }
    }
}