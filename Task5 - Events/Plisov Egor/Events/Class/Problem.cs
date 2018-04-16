using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events
{
    class Problem : IProblem
    {
        public int Xpos { get; set; }
        public int Ypos { get; set; }

        public void WatchMoverTurn(IMover mover)
        {
            mover.Move += this.OnMoverTurn;
        }

        private void OnMoverTurn(object sender, EventArgs e)
        {
            var args = (MoverTurnEventArgs)e;

            if ((args.NewX == Xpos) && (args.NewY == Ypos))
            {
                Console.Clear();
                Console.WriteLine("Damn!");
            }
        }
    }
}
