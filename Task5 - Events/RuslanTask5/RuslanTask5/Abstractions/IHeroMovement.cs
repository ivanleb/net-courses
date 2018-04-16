using System;

namespace RuslanTask5.Abstractions
{
    interface IHeroMovement
    {
        //bool IsNextMove(object key, EventArgs args);
        //void OnNextMove(object key, EventArgs args);
        void StartListen(IInputProcess input);
    }
}
