using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Drawing.Core.Abstractions
{
    public interface IProcessUserActions
    {
        void SetBoardSize(IBoard board);
        int GetChosenAction();
        void QuitGame();
    }
}
