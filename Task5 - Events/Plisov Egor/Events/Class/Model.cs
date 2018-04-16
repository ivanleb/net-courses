using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events
{
    class Model : IModel
    {
        public IMover Mover { get ; set; }
        public IList<IProblem> problem { get; set; }

        public Model()
        {
            this.Mover = new Mover { Xpos = 10, Ypos = 10 };

            var rand = new Random();
            this.problem = this.problem = new List<IProblem>(3);
            for (int i = 0; i < 3; i++)
            {
                this.problem.Add(
                    new Problem
                    {
                        Xpos = rand.Next(1, StaticRegistry.board.Width - 1),
                        Ypos = rand.Next(1, StaticRegistry.board.Height - 1)
                    });
            }
        }
    }
}
