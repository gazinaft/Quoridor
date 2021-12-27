using View;
using Controllers;
using Model;
namespace ConsoleTestProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game(new ABStrategy(new ABTree(2)));

            ConsoleView consoleView = new ConsoleView();

            Presenter presenter = new Presenter(consoleView, game);
            
        }
    }
}
