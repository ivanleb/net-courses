namespace Events.Core.Abstractions
{
    public interface IMine
    {
        int X { get; set; }
        int Y { get; set; }
        char Mark { get; }
        void WatchHeroMove(IHero hero);
    }
}
