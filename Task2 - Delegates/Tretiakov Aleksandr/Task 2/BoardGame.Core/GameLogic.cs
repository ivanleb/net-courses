using System;
using BoardGame.Core.Abstractions;
using BoardGame.Core.Models;

namespace BoardGame.Core
{
    public class AppLogic
    {
        Board _board;
        IMessager _messager;
        IBoardHandler _boardHandler;
        IProcessUserInput _processUserInput;

        public AppLogic(IMessager messager, IBoardHandler boardHandler, IProcessUserInput processUserInput)
        {
            _messager = messager ?? throw new ArgumentNullException();
            _boardHandler = boardHandler ?? throw new ArgumentNullException();
            _processUserInput = processUserInput ?? throw new ArgumentNullException();
        }

        public void Run(Board board)
        {
            _board = board;
            while (true)
            {
                _boardHandler.DrawBoard(board);
                _messager.ShowInfromtaion("1. Draw vertical line.\n2. Draw horizontal line.\n3. Draw point.\n4. Clear board.");
                var input = _processUserInput.GetUserChoise();
                switch (input)
                {
                    case UserChoise.VerticalLine:
                        board.IsVerticalLineShown = true;
                        break;
                    case UserChoise.HorizontalLine:
                        board.IsHorizontalLineShown = true;
                        break;
                    case UserChoise.Point:
                        board.IsPointShown = true;
                        break;
                    case UserChoise.ClearBoard:
                        board.IsHorizontalLineShown = false;
                        board.IsVerticalLineShown = false;
                        board.IsPointShown = false;
                        break;
                    default:
                        throw new ArgumentException($"Sth bad happend. There's no {input} in settings");
                }
            }
        }
    }
}
