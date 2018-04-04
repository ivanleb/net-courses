using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame.Core.Abstractions
{
    public interface IProcessUserInput
    {
        UserChoise GetUserChoise();
    }

    public enum UserChoise
    {
        VerticalLine,
        HorizontalLine,
        Point,
        ClearBoard
    }
}
