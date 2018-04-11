using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public interface IGameModel
    {
        HashSet<IObject> Objects { get; set; }

        Point BoardSize { get; set; }

        IHero Hero { get; set; }

        void AddObject(IObject obj);
        void DeleteObject(IObject obj);

        void Clear();

        void GenerateRandomLevel();

        void MoveHero(Point Dir);
    }

    public interface IBoard
    {
        IGameModel GameModel { get; set; }

        void DrawBoard();

        void DrawObjectOnBoard(IObject obj);
    }

    public interface IController
    {
        event Action<Point> OnMotionHero;

        int ProcessUserInput();
    }


    public interface IUserInterface
    {
        void ShowMessage(string message);

    }

    public interface IRegistry
    {
        IUserInterface UserInterface { get; set; }
        IBoard Board { get; set; }

        IController Controller { get; set; }

    }

    public class CollisionEventArgs
    {
        public IObject target;
    }

    public class Point
    {
        public int X, Y;
         
        public Point(int newX, int newY)
        {
            X = newX;
            Y = newY;
        }

        public static Point operator +(Point P1, Point P2)
        {
            Point P3 = new Point(P1.X, P1.Y);
            P3.X += P2.X;
            P3.Y += P2.Y;

            return P3;
        }

        public static Point operator -(Point P1, Point P2)
        {
            Point P3 = new Point(P1.X, P1.Y);

            P3.X -= P2.X;
            P3.Y -= P2.Y;

            return P3;
        }

        public static bool operator !=(Point P1, Point P2)
        {
            if (P1.X != P2.X || P1.Y != P2.Y)
                return true;

            return false;
        }

        public static bool operator ==(Point P1, Point P2)
        {
            if (P1.X == P2.X && P1.Y == P2.Y)
                return true;

            return false;
        }
    }

    public interface IObject
    {
        Point Coords { get; set; }

        bool IsCollision(IObject obj);

        void CollisionReaction();
    }

    public interface IHero : IObject
    {
        //event Action<IObject, CollisionEventArgs> Collision;
    }

}
