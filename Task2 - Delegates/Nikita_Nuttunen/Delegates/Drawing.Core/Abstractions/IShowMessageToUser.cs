using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Drawing.Core.Abstractions
{
    public interface IShowMessageToUser
    {
        void ShowMessage(string message);
        void DrawBoard(IBoard board);
        void ClearBoard();
        void ClearScreen();
        void ClearInput();
    }
}
