using Core;
using EPAM_homework_events.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM_homework_events
{
    class ConsoleHero : IHero, IConsoleObject
    {
        public Point Coords { get; set; }
        public ConsoleColor Color { get; set; }
        public char Model { get; set; }

        public ConsoleHero(Point newCoords, char newModel)
        {
            Coords = newCoords;
            Model = newModel;
            Color = ConsoleColor.Green;
        }

        public void CollisionReaction()
        {
        }

        public bool IsCollision(IObject obj)
        {
            return false;
        }
    }
}
