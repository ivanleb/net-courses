namespace HeroesCore.Abstractions
{
    public interface IRegistery
    {
        IBoard Board { get; set; }
        IModel Model { get; set; }
        IUserIteraction UserIteraction { get; set; }
        IHero Heroes { get; set; }
    }
}
