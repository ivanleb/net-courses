using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace EPAM_homework_events.Objects
{
    class ConsoleBomb : IConsoleObject
    {
        public event Action<IObject> OnExploded;
        public ConsoleColor Color { get; set; }
        public char Model { get; set; }
        public Point Coords { get; set; }

        public ConsoleBomb(Point newCoords, char newModel)
        {
            Coords = newCoords;
            Model = newModel;
            Color = ConsoleColor.Red;
        }

        public void CollisionReaction()
        {
            OnExploded(this);
        }

        public bool IsCollision(IObject obj)
        {
            if (obj.Coords.X == Coords.X && obj.Coords.Y == Coords.Y)
                return true;

            return false;
        }
    }
}
