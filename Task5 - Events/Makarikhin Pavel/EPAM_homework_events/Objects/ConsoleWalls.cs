using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM_homework_events.Objects
{
    class ConsoleWall : IConsoleObject
    {
        public Point Coords { get; set; }
        public Point EndCoords { get; set; }
        public ConsoleColor Color { get; set; }
        public char Model { get; set; }

        public ConsoleWall(Point newCoords, Point newEndCoords, char newModel)
        {
            Coords = newCoords;
            EndCoords = newEndCoords;

            Model = newModel;

            Color = ConsoleColor.White;
        }

        public void CollisionReaction()
        {
            Color = ConsoleColor.DarkGreen;
        }

        public bool IsCollision(IObject obj)
        {
            if ((obj.Coords.X == Coords.X && EndCoords.X == obj.Coords.X) ||
                    (obj.Coords.Y == Coords.Y && EndCoords.Y == obj.Coords.Y))
                return true;

            if (Color == ConsoleColor.DarkGreen)
                Color = ConsoleColor.White;

            return false;
        }
    }
}
