using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsCore
{
    public class MovingArgs : EventArgs
    {
    }
    public delegate void MovingHandler(IHero sender, MovingArgs args);
    public interface IHero
    {
        void Move(ConsoleKeyInfo command);
        event MovingHandler Moving;
    }

    public class ExplosionArgs : EventArgs
    {
    }
    public delegate void ExplosionHandler(IMine sender, ExplosionArgs args);
    public interface IMine
    {
        int x { get; set; }
        int y { get; set; }
        event ExplosionHandler Explosion;
        void TrackHero(IHero hero, MovingArgs args);
    }


    public interface Model
    {
        //public void AddHero(IHero NewHero)
        //{
        //    Heros.Add(NewHero);
        //    foreach (IMine mine in Mines)
        //    {
        //        NewHero.Moving += mine.TrackHero;
        //    }
        //    NewHero.Moving += Board.TrackHero;

        //}
        //public void AddMine(IMine NewMine)
        //{
        //    Mines.Add(NewMine);
        //    foreach (IHero hero in Heros)
        //    {
        //        hero.Moving += NewMine.TrackHero;
        //    }
        //}
        public void Initialize();
        //{
        //    foreach (IHero hero in Heros)
        //    {
        //        foreach (IMine mine in Mines)
        //        {
        //            hero.Moving += mine.TrackHero;
        //        }
        //        hero.Moving += Board.TrackHero;
        //    }
        //}
        bool CanMoveHero(ConsoleKeyInfo key);
        IBoard Board { get; set; }
        IHero Hero { get; set; }
        List<IMine> Mines { get; set; }
    }

    public interface IBoard
    {
        int sizeX { get; set; }
        int sizeY { get; set; }
        void Draw();
        void TrackHero(IHero hero, MovingArgs args);
    }
    public interface IRegistry
    {
        Model model { get; set; }
        IUserInput userInput { get; set; }
    }
    public interface IUserInput
    {
        ConsoleKeyInfo GetUserInput();
        void BroadcastInput();
    }
    public sealed class GameCore
    {
        public void Run(IRegistry registry)
        {
            var model = registry.model;
            var input = registry.userInput;
            while (true)
            {
                var command = input.GetUserInput();
                if (model.CanMoveHero(command)) model.Hero.Move(command);

            }
        }

        private void Input_InputEvent(ConsoleKeyInfo key)
        {
            throw new NotImplementedException();
        }
    }
}
