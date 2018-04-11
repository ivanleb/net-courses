using System;
using System.Collections.Generic;

namespace Events.Core
{
    internal delegate void UserInputKeyEvent(object sender, UserInputEventArgs args);

    internal delegate void EventHandler(object sender, EventArgs args);

    internal delegate void BumpToBorder(object sender, BooleanEventArgs args);

    internal class UserInputEventArgs : EventArgs
    {
        public ConsoleKey Key { get; set; }
    }

    internal class BooleanEventArgs : EventArgs
    {
        public bool ok { get; set; }
    }

    internal interface IUserAction
    {
        event UserInputKeyEvent InputNotification;
        void OnExplosion(object sender, EventArgs args);
        void StartNotification();
    }

    internal interface IBoard : IDrawable
    {
        int SizeX { get; set; }
        int SizeY { get; set; }
        void CheckHeroPosition(object sender, EventArgs args);
        event BumpToBorder Bump;
    }

    internal interface IHero : IDrawable
    {
        IPoint Position { get; set; }
        IPoint NextPosition { get; }
        event EventHandler HeroEvent;
        void HeroMove(object sender, UserInputEventArgs args);
        void Switch(object sender, BooleanEventArgs args);
        void AndNowImDead(object sender, EventArgs args);
    }

    internal interface IPoint
    {
        int X { get; set; }
        int Y { get; set; }
    }

    internal interface IMine
    {
        IPoint Position { get; set; }
        void CheckHeroPosition(object sender, EventArgs args);
        event EventHandler Boom;
        void Draw();
    }

    internal interface IMines
    {
        List<IMine> FewMines { get; set; }
        void OnExplosion(object sender, EventArgs args);
    }

    internal interface IRegistry
    {
        IBoard board { get; set; }
        IHero hero { get; set; }
        IMines mines { get; set; }
        IMine mine { get; set; }
        IUserAction user { get; set; }
    }
    internal interface IDrawable
    {
        void Draw();
        void WriteAt(string s, int x, int y);
    }
}