using System;
using System.Collections.Generic;
using BoardGame.Core;
using Task5.Core.Models;

namespace Task5.Core.Abstractions
{
    public interface IBoardHandler
    {
        Hero Hero { get; set; }
        Board Board { get; set; }
        IEnumerable<Bomb> Bombs { get; set; }
        void DrawAll();
        bool IsValidMove(UserChoise userChoise);
    }
}