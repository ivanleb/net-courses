using System;

namespace Events.Core
{
    public class GameLogic
    {
        internal static void Run(IRegistry registry)
        {
            var board = registry.board;
            var hero = registry.hero;
            var mines = registry.mines;
            var user = registry.user;

            Console.CursorVisible = false;
            board.Draw();
            hero.Draw();

            user.InputNotification += hero.HeroMove;
            hero.HeroEvent += board.CheckHeroPosition;
            board.Bump += hero.Switch;

            foreach (var mine in mines.FewMines)
            {
                mine.Draw();
                hero.HeroEvent += mine.CheckHeroPosition;
                mine.Boom += user.OnExplosion;
                mine.Boom += hero.AndNowImDead;
                mine.Boom += mines.OnExplosion;
            }
            user.StartNotification();
        }
    }
}