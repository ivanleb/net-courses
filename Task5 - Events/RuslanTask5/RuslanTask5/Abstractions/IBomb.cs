namespace RuslanTask5.Abstractions
{
    interface IBomb
    {
        int PositionX { get; set; }
        int PositionY { get; set; }
        char Marker { get; set; }
        void StartListening(IInputProcess input);
    }
}
