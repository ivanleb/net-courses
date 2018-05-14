namespace ExceptionsAndLogging.Abstractions
{
    interface IPoint
    {
        decimal X { get; set; }
        decimal Y { get; set; }
    }

    struct Point : IPoint
    {
        public decimal X { get; set; }
        public decimal Y { get; set; }

        public override string ToString()
        {
            return $"X = {X}, Y = {Y}";
        }
    }
}
