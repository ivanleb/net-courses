using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Core.Abstractions
{
    public interface IBoard
    {
        int SizeX { get; set; }
        int SizeY { get; set; }
        void SetupBoard(int sizeX, int sizeY);
        void Draw(IModel model);
        void StartListenInput(IUserInteraction userInteraction);
    }
}
