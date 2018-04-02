using System;
using System.Linq;

namespace Delegates
{
    internal class Program
    {
        private static void Main()
        {
            IBoard board = new Board();
            IUserAction userAction = new UserIntaraction();
            IUtils utils = new Utils();
            board.SetSize();
            board.Draw(board);
            var typeList = typeof(IBoard).Assembly.GetTypes()
                .Where(x => x.GetInterfaces().Contains(typeof(IBoard)))
                .Where(x => x.Name != board.GetType().Name)
                .OrderByDescending(x => x.Name)
                .ToList();
            var objectsList = typeList.Select(x => (IBoard) Activator.CreateInstance(x)).ToList();

            while (true)
            {
                utils.WriteAt("", 0, board.GetHeight() + 1);
                userAction.ShowTypes(objectsList);
                var selectedType = userAction.GetInt("Выберете объект для построения", 0, objectsList.Count);
                Draw draw = null;
                if (selectedType == 0) break;
                draw += objectsList[selectedType - 1].Draw;
                draw(board);
            }
        }

        private delegate void Draw(IBoard board);
    }
}