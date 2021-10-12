using Model;
using NUnit.Framework;

namespace TestProject1
{
    public class GameFieldTests
    {

    [Test]
        public void SetBlock_Places_Wall()
        {
            //arrange
            var direct = new GameField(9, 9);
            //act
            var result = direct.SetBlock(5, 5, false);
            //assert
            Assert.Pass();
        }

        [Test]
        public void MovePlayer_Can_Place()
        {
            //arrange 
            var pmove = new GameField(9, 9);
            //act

            //assert
        }
    }
    
}