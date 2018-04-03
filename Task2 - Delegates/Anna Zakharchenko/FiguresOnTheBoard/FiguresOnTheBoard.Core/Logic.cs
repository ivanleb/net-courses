using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiguresOnTheBoard.Core.Abstractions;

namespace FiguresOnTheBoard.Core
{
    public class Logic
    {
        public void Run(IRegistry registry)
        {
            IBoard board = registry.Board;
            IProcessUserInput processUserInput = registry.ProcessUserInput;
            IShowMessageToUser showMessageToUser = registry.ShowMessageToUser;
            IExecuteUserChoice executeUserChoice = registry.ExecuteUserChoice;
            IDrawing drawing = registry.Drawing;

            board.SetBoardSize(40, 20);
            drawing.DrawBoard(board);
            showMessageToUser.ShowHelloToUser();

            while (true)
            {
                int currentChoice = processUserInput.GetChoice(); 
                if (executeUserChoice.IsExit(currentChoice)) break; 
                if (!executeUserChoice.IsValid(currentChoice))
                {
                    showMessageToUser.ShowInstructionForUser("Ivalid input! Please, try again, choose from the options.");
                    continue;
                }
                executeUserChoice.MakeFigure(board, drawing, currentChoice);    
            }

        }
    }
}
