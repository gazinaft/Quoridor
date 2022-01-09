using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using UnityEngine;

namespace View
{
    public class UnityGameView : MonoBehaviour, IGameView
    {

        Dictionary<GameObject, (int, int)> _spriteToCoordinateDictionary;

        Dictionary<(int, int), GameObject> coordinateToObjectDictionary;

        public UnityGameView()
        {

        }

        public bool SelectedWallIsHorizontal { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int SelectedCellX { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int SelectedCellY { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int SelectedCornerX { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int SelectedCornerY { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event Action PlacingTheWall;
        public event Action PlayerMove;
        public event Action ChangePlayer;
        public event Action DoUndo;

        public void CantPlaceTheWall()
        {
            throw new NotImplementedException();
        }

        public void DisplayPotentialWallsAndCorners(GameFieldState state)
        {
            throw new NotImplementedException();
        }

        public void DisplayTheField(GameFieldState state)
        {
            throw new NotImplementedException();
        }

        public void PlaceTheWall(GameFieldState state)
        {
            throw new NotImplementedException();
        }

        public void ThisIsTheEnd()
        {
            throw new NotImplementedException();
        }
    }
}
