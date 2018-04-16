using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events
{
    interface IBoard
    {
        int Height { get; set; }
        int Width { get; set; }

        void Draw();
        void StartListenInput(IUserInput input);
    }
}
