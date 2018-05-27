using System;
using System.Diagnostics.Tracing;

namespace EF.Core.Abstractions
{
    public interface IUserInput
    {
        event EventHandler KeyPressed;
        
        void ListenToUser();
    }
}