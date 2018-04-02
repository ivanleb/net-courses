using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates.Core.Abstractions
{
    public delegate void Draw(IBoard board);

    public interface IBoard
    {
        int BoardSizeX { get; set; }
        int BoardSizeY { get; set; }

        void SetBoardSize(int x, int y);
    }
}
