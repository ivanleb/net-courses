﻿namespace Events.Core.Abstractions
{
    public interface IBoard //: IDrawing
    {
        int Width { get; set; }
        int Height { get; set; }

        void Draw();
        void StartListeningInput(IUserInteraction input);
    }
}