using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardDrawing.Core.Abstractions
{
    public interface IBoard
    {
        int BoardSizeX { get; set; }
        int BoardSizeY { get; set; }
        void Draw(IModel model);
        void PrepareBoard(int sizeX, int sizeY);
        void BoardClear();
        void StartListenInput(IUserInteraction input);
    }
}
