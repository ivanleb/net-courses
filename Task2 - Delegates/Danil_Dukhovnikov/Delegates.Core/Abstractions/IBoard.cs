using System.Collections.Generic;

namespace Delegates.Core.Abstractions
{
    public interface IBoard
    {
        int StartX { get; }
        int StartY { get; }
        int BoardSizeX { get; }
        int BoardSizeY { get; }
        IList<IPoint> BoardPoints { get; }
        
        void DrawBoard(DrawType drawType);
    }
}