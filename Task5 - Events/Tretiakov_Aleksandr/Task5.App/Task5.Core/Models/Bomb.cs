using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5.Core.Models
{
    public class Bomb
    {
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public string Symbol { get; set; }

        public event EventHandler BombExploded;

        public void OnHeroMove(object sender, Hero e)
        {
            var isHeroOnBomb = e.PositionX == this.PositionX && e.PositionY == this.PositionY;
            if (isHeroOnBomb)
            {
                BombExploded?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
