namespace ExceptionsAndLogging.Abstractions
{
    internal interface IPoint
    {
        decimal X { get; set; }
        decimal Y { get; set; }
    }

    internal struct Point : IPoint
    {
        public decimal X { get; set; }
        public decimal Y { get; set; }

        public override string ToString()
        {
            return $"X = {X}, Y = {Y}";
        }
    }
}
