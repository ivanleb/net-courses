using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardDrawing.Core.Abstractions
{
    public interface IShowMessageToUser
    {
        string Greetings { get; set; }
        void ShowMessage(string message);
        void Pause();
    }
}
