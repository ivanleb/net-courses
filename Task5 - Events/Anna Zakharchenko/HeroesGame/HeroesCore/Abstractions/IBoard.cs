namespace HeroesCore.Abstractions
{
    public interface IBoard
    {
        int SizeX { get; set; }
        int SizeY { get; set; }
        void SetUpBoard(int sizeX, int sizeY);
        void Draw(IModel model);
        void StartListenInput(IUserIteraction input);
    }
}
