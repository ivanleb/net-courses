using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame.Core.Models
{
    public class Board
    {
        public int SizeX { get; set; }
        public int SizeY { get; set; }
        public bool IsPointShown { get; set; }
        public bool IsVerticalLineShown { get; set; }
        public bool IsHorizontalLineShown { get; set; }
    }
}
