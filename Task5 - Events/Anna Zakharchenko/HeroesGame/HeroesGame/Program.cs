using HeroesGame.Implementations;
using HeroesCore;

namespace HeroesGame
{
    class Program
    {
        static void Main(string[] args)
        {
            new Core().Run(new ConsoleAppRegistery());
        }
    }
}
