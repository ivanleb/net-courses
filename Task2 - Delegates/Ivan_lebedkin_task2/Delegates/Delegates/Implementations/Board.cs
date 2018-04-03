using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delegates.Core.Abstractions;

namespace Delegates.Implementations
{
    class Board : IBoard
    {
        public Board()
        {
            this.boardSizeX = 20;
            this.boardSizeY = 20;
            objects = new List<IDrawingObject>();
        }
        public int boardSizeX { get; set; }
        public int boardSizeY { get; set; }
        List<IDrawingObject> objects;

        public void AddObject(IDrawingObject obj)
        {
            objects.Add(obj);
        }

        public IEnumerable<IDrawingObject> GetObjects()
        {
            return objects;
        }

        public void DeleteObject(IDrawingObject obj)
        {
            objects.Remove(obj);
        }
    }
}
