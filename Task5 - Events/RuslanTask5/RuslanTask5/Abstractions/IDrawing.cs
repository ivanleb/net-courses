namespace RuslanTask5.Abstractions
{
    interface IDrawing
    {
        void DrawBoard(IBoard board);
        void DrawHero(IHero hero);
        void BoardReactionOnHero(IBoard board, IHero hero);
    }
}
