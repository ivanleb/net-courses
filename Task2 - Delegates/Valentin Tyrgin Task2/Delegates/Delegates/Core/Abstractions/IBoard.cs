using System.Collections.Generic;

namespace Delegates.Core.Abstractions
{
    public interface IBoard
    {
        string Name { get; set; }
        void SetSize();
        int Height { get; set; }
        int Width { get; set; }
        void Draw(IBoard board);
    }

    public interface IUserAction
    {
        int GetInt(string description, int min = int.MinValue, int max = int.MaxValue); //запрос числа из диапазона
        void ShowTypes(List<IBoard> iBoardList); //вывод доступных для построения типов
    }

    public interface IUtils
    {
        void WriteAt(string s, int x, int y);
        void WriteOutsideBoard(IBoard board, string s);
        void Clean();
    }

    public interface ITextQuery
    {
        string IntQuery { get; set; }
        string SelectionQuery { get; set; }
    }
}