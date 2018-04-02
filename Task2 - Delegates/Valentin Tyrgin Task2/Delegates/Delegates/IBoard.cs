using System.Collections.Generic;

namespace Delegates
{
    internal delegate void Draw(IBoard board);

    internal interface IBoard
    {
        string GetName();
        void SetSize();
        int GetHeight();
        int GetWidth();
        void Draw(IBoard board);
    }

    internal interface IUserAction
    {
        int GetInt(string description, int min = int.MinValue, int max = int.MaxValue); //запрос числа из диапазона
        void ShowTypes(List<IBoard> iBoardList); //вывод доступных для построения типов
    }

    internal interface IUtils
    {
        void WriteAt(string s, int x, int y);
    }
}