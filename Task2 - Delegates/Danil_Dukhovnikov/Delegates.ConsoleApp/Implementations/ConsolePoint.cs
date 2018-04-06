using System;
using Delegates.Core.Abstractions;

namespace Delegates.ConsoleApp.Implementations
{
    public class ConsolePoint : IPoint
    {
        public ConsolePoint(int x, int y, string content)
        {
            
            X = x;
            Y = y;
            Content = content;
        }

        public int X { get; }
        public int Y { get; }
        public string Content { get; }
        
        public void Draw()
        {
            Console.SetCursorPosition(X, Y);
            Console.Write(Content);
        }
    }
}