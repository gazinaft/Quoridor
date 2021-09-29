using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using View;
namespace Controllers
{
    public class Presenter
    {
        private IGameView View;

        public Presenter(IGameView view)
        {
            View = view;

            View.PlacingTheWall += PlaceTheWall;

            View.PlayerMove += MakeStep;
        }

        public void MakeStep() {

            throw new NotImplementedException();

        }

        public void PlaceTheWall() {

            throw new NotImplementedException();

        }


    }
}
