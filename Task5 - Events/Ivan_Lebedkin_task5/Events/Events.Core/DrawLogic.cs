using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Events.Core.Abstractions;

namespace Events.Core
{
    public class DrawLogic
    {       
        public void Run(IRegistry registry)
        {
            var showDrawingToUser = registry.ShowDrawingToUser;
            var processUserInput = registry.ProcessUserInput;
            var currentBoard = registry.GetEmptyBoard;

            //обработчик для нажатия стрелочки, определяет какая именно стрелочка нажата и перерисовывает доску
            processUserInput.Shift += showDrawingToUser.ShiftHandler;
            currentBoard.Hit += showDrawingToUser.HitHandler;
            showDrawingToUser.DrawBoard(currentBoard);
            while (true)
            {
                processUserInput.Input(currentBoard);
            }
        }
    }
}
