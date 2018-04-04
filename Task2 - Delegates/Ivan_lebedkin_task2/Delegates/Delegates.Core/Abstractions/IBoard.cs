using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates.Core.Abstractions
{
    public interface IBoard
    {
        Int32 boardSizeX { get; set; }
        Int32 boardSizeY { get; set; }

        void AddObject(IDrawingObject obj);
        void DeleteObject(IDrawingObject obj);
        IEnumerable<IDrawingObject> GetObjects();
    }
}
