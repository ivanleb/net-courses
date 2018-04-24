using Exceptions_and_Logging.Abstractions;

namespace Exceptions_and_Logging.Implementations
{
    class Point : IPoint
    {
        public decimal X { get; set; }
        public decimal Y { get; set; }

        public override string ToString()
        {
            return $"X = {X}, Y = {Y}";
        }
    }
}
