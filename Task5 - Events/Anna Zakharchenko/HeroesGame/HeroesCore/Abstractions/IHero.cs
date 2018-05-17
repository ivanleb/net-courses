namespace HeroesCore.Abstractions
{
    public interface IHero
    {
        int PosX { get; set; }
        int PosY { get; set; }
        string MarkSymbol { get;}
        void StartListenInput(IUserIteraction input);
    }
}
