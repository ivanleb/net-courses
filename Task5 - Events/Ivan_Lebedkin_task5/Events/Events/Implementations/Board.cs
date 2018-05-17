using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Events.Core.Abstractions;

namespace Events.Implementations
{
    class Board : IBoard, IHaveMines
    {
        public Board()
        {
            this.boardSizeX = 20*2;
            this.boardSizeY = 20;
            this.Mines = new List<Mines>();
            Random rnd = new Random();
            Int32 size = rnd.Next(5, 10);
            for (int i = 0; i < size; i++)
            {
                ((List<Mines>)this.Mines).Add(
                    new Mines(
                        rnd.Next(1, boardSizeX ), 
                        rnd.Next(1, boardSizeY))
                    );
            }

            this.Point = new Point(this.boardSizeX / 2, this.boardSizeY / 2);
        }
        public int boardSizeX { get; set; }
        public int boardSizeY { get; set; }

        private IGeometryObject point;
        public IGeometryObject Point {
            get
            {
                return point;
            }
            set
            {
                point = value;
                IsMineWasPressed();
            }
        }
        public IEnumerable<IGeometryObject> Mines { get; set; }

        public event Action Hit
        {
            add
            { hit += value; }
            remove
            { hit -= value; }
        }
        public Action hit;
        
        public void IsMineWasPressed()
        {
            foreach (IGeometryObject mine in this.Mines)
            {
                if (mine.X == Point.X & mine.Y == Point.Y)
                {
                    hit();
                }
            }            
        }
    }
}
