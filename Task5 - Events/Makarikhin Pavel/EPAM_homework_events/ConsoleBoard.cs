using Core;
using EPAM_homework_events.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM_homework_events
{
    class ConsoleBoard : IBoard
    {
        public IGameModel GameModel { get; set; }

        public static Point BoardOrigin { get; set; }

        public ConsoleBoard(IGameModel newGameModel)
        {
            BoardOrigin = new Point(0, 5);
            GameModel = (ConsoleGameModel)newGameModel;
        }

        private static void WriteAt(string str, Point point)
        {
            try
            {
                Console.SetCursorPosition(BoardOrigin.X + point.X, BoardOrigin.Y + point.Y);
                Console.Write(str);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }

        public void DrawBoard()
        {
            foreach (IObject obj in GameModel.Objects)
                DrawObjectOnBoard(obj);

            DrawObjectOnBoard(GameModel.Hero);
        }

        public void DrawObjectOnBoard(IObject obj)
        {
            Console.ForegroundColor = ((IConsoleObject)obj).Color;

            if (obj.GetType() == typeof(ConsoleWall))
            {
                for (int i = 0; i <= ((ConsoleWall)obj).EndCoords.X - obj.Coords.X; i++)
                    for (int j = 0; j <= ((ConsoleWall)obj).EndCoords.Y - obj.Coords.Y; j++)
                        WriteAt(((IConsoleObject)obj).Model.ToString(), obj.Coords + new Point(i, j));
            }

            WriteAt(((IConsoleObject)obj).Model.ToString(), obj.Coords);

            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
