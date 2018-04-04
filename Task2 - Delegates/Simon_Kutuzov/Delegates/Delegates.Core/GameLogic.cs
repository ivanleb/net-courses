using Delegates.Core.Abstractions;

namespace Delegates.Core
{
    public class GameLogic
    {
        delegate void Draw(IBoard board);
        Draw draw = null;

        public void Run(IRegistry registry)
        {
            var board = registry.Board;
            var drawing = registry.Drawing;
            var stringOutput = registry.StringOutput;
            var userActions = registry.UserActions;

            stringOutput.ShowMessage("Hi!\n");
            stringOutput.ShowMessage("Please set board dimentions: ");
            userActions.SetBoardSize(board);
            drawing.Reset(board);
            stringOutput.ShowMenu();

            int selection;
            do
            {
                selection = userActions.SelectMenuOption();
                switch (selection)
                {
                    case 1:
                        {
                            draw = drawing.DrawDot;
                            break;
                        }
                    case 2:
                        {
                            draw = drawing.DrawVecticalLine;
                            break;
                        }
                    case 3:
                        {
                            draw = drawing.DrawHorizontalLine;
                            break;
                        }
                    case 4:
                        {
                            draw += drawing.DrawVecticalLine;
                            draw += drawing.DrawHorizontalLine;
                            break;
                        }
                    case 5:
                        {
                            drawing.Reset(board);
                            stringOutput.ShowMenu();
                            draw = null;
                            break;
                        }
                    default:
                        {
                            draw = null;
                            break;
                        }
                }

                draw?.Invoke(board);

            } while (selection != 0);
        }
    }
}
