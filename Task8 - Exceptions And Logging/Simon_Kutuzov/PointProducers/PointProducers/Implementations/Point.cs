using PointProducers.Abstractions;

namespace PointProducers.Implementations
{
    public class Point : IPoint
    {
        public double X { get; set; }
        public double Y { get; set; }

        public override string ToString()
        {
            return $"X = {X}, Y = {Y}";
        }
    }
}
