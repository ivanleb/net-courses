using Core;
using EPAM_homework_events.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM_homework_events
{
    class ConsoleGameModel : IGameModel
    {
        public event Action<TimeSpan> OnBoom;

        public HashSet<IObject> Objects { get; set; }
        public IHero Hero {get; set;}
        public Point BoardSize { get; set; }

        public ConsoleGameModel(Point newBoardSize)
        {
            Objects = new HashSet<IObject>();

            BoardSize = newBoardSize;
        }

        public void AddObject(IObject obj)
        {
            Objects.Add(obj);
        }

        public void DeleteObject(IObject obj)
        {
            Objects.Remove(obj);
        }

        public void Clear()
        {
            Objects.Clear();
        }

        public void BombDestroyed(IObject bomb)
        {
            Objects.Remove(bomb);
            OnBoom(DateTime.Now.TimeOfDay);
        }

        public void GenerateRandomLevel()
        {
            Random rand = new Random();

            Hero = new ConsoleHero(new Point(rand.Next() % BoardSize.X, rand.Next() % BoardSize.Y), 'H');

            Objects.Add(new ConsoleWall(new Point(0, 0), new Point(BoardSize.X, 0), '─'));
            Objects.Add(new ConsoleWall(new Point(0, BoardSize.Y), new Point(BoardSize.X, BoardSize.Y), '─'));
            Objects.Add(new ConsoleWall(new Point(0, 0), new Point(0, BoardSize.Y), '│'));
            Objects.Add(new ConsoleWall(new Point(BoardSize.X, 0), new Point(BoardSize.X, BoardSize.Y), '│'));

            for (int i = 0; i < 100; i++)
            {
                Point coords = new Point(rand.Next() % (BoardSize.X - 1) + 1, rand.Next() % (BoardSize.Y - 1) + 1);

                if (!((from IObject obj in Objects where (obj.Coords == coords) select obj).ToArray().Length > 0))
                {
                    ConsoleBomb bomb = new ConsoleBomb(coords, 'X');
                    bomb.OnExploded += BombDestroyed;
                    Objects.Add(bomb);
                }
            }
        }

        public void OutOfBoardCorrecting(Point Dir)
        {
            Hero.Coords -= Dir; 
        }

        public void MoveHero(Point Dir)
        {
            Hero.Coords += Dir;

            if (Hero.Coords.X < 0 || Hero.Coords.X > BoardSize.X ||
                    Hero.Coords.Y < 0 || Hero.Coords.Y > BoardSize.Y)
                OutOfBoardCorrecting(Dir);

            IObject collisionObject = (from IObject obj in Objects where obj.IsCollision(Hero) select obj).FirstOrDefault();

            if (collisionObject != null)
            {
                Hero.CollisionReaction();
                collisionObject.CollisionReaction();
            }
        }
    }
}
