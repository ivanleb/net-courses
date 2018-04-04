using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delegates.Core.Abstractions;

namespace Delegates.Core
{
    public delegate void Draw(IBoard board);
    public class DrawLogic
    {
        
        public Draw currentDrawDelegate;

        public void Run(IRegistry registry)
        {
            var showDrawingToUser = registry.ShowDrawingToUser;
            var processUserInput = registry.ProcessUserInput;
            var currentBoard = registry.GetEmptyBoard;
            currentDrawDelegate += showDrawingToUser.Draw;

            while (true)
            {
                var currentObject = processUserInput.InputObject();
                if (currentObject is null)
                {
                    processUserInput.DeleteObjectFromBoard(currentBoard);
                }
                else
                {
                    currentBoard.AddObject(currentObject);
                }
                
                currentDrawDelegate(currentBoard);
            }
        }

    }
}
