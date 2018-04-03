using System;
using System.Linq;
using Delegates.Core.Abstractions;

namespace Delegates.Core
{
    public class AppLogic
    {
        private delegate void Draw(IBoard board);

        public static void Run(IRegistry registry)
        {
            var board = registry.Board;
            var user = registry.User;
            var utils = registry.Utils;
            var userText = registry.Text;

            board.SetSize();
            board.Draw(board);

            var typeList = typeof(IBoard).Assembly.GetTypes()
                .Where(x => x.GetInterfaces().Contains(typeof(IBoard)))
                .Where(x => x.Name != board.GetType().Name)
                .OrderByDescending(x => x.Name)
                .ToList();
            var objectList = typeList.Select(x => (IBoard) Activator.CreateInstance(x)).ToList();
            Draw temp = null;
            Draw draw = null;
            while (true)
            {
                utils.WriteOutsideBoard(board, userText.SelectionQuery);
                user.ShowTypes(objectList);
                var selectedType = user.GetInt(userText.IntQuery, 0, objectList.Count);
                if (selectedType == 0) break;
                utils.Clean();
                board.Draw(board);
                temp = draw ?? temp;
                draw = null;
                if (temp != null)
                {
                    draw = temp.GetInvocationList()
                        .Distinct()
                        .Cast<Draw>()
                        .Aggregate(draw, (current, next) => current + next);
                }
                draw += objectList[selectedType - 1].Draw;
                draw(board);
            }
        }
    }
}