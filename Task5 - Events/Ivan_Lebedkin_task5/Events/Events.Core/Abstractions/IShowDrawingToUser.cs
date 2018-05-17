using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Core.Abstractions
{
    public interface IShowDrawingToUser
    {
        Action<IBoard> DrawSmth { get; set; }
        void DrawBoard(IBoard board);
        void ShiftHandler(object o, EventArgs e);
        void HitHandler();
        
    }
}
