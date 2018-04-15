using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task5.Core.Abstractions;

namespace Task5.Core.Models
{
    public class Hero
    {
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public string Symbol { get; set; }

        public event EventHandler<Hero> Moved;

        public void OnMove(object sender, UserChoise e)
        {
            switch (e)
            {
                case UserChoise.Down:
                    PositionY -= 1;
                    break;
                case UserChoise.Up:
                    PositionY += 1;
                    break;
                case UserChoise.Left:
                    PositionX -= 1;
                    break;
                case UserChoise.Right:
                    PositionX += 1;
                    break;
            }
            Moved?.Invoke(this, this);
        }
    }
}
