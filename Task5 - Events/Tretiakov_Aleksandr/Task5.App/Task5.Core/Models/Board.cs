using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardGame.Core;
using Task5.Core.Abstractions;

namespace Task5.Core.Models
{
    public class Board
    {
        public int SizeX { get; set; }
        public int SizeY { get; set; }
        public bool IsBottomWallTouched { get; private set; } = false;
        public bool IsTopWallTouched { get; private set; } = false;
        public bool IsLeftWallTouched { get; private set; } = false;
        public bool IsRightWallTouched { get; private set; } = false;

        public void OnMove(object sender, Hero e)
        {
            IsRightWallTouched = e.PositionX == SizeX - 1;
            IsLeftWallTouched = e.PositionX == 1;
            IsTopWallTouched = e.PositionY == 1;
            IsBottomWallTouched = e.PositionY == SizeY - 1;
        }
    }
}
