using System;

namespace EF.Core.Abstractions
{
    public interface IUserInputHandler
    {
        void ProcessUserCommand(object sender, EventArgs args);
        ConsoleKey Stop { get; set; }
    }
}