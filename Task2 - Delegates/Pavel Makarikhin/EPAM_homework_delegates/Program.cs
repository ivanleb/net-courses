using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Board;

namespace EPAM_homework_delegates
{
    class Program
    {
        delegate void Draw(IBoard board);

        static void Main(string[] args)
        {
            new ProgramLogic().Run(new Registry());
        }
    }
}
