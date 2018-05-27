using System;

namespace ORM.Core.Abstractions
{
    public interface IUserInput
    {
        void ListenToUser();
        event EventHandler<ConsoleKey> OnUserInputRecieved;
    }
}
