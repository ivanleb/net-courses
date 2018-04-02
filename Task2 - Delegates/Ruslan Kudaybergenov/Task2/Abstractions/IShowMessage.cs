using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.Abstractions
{
    public interface IShowMessageToUser
    {
        void Message(string message);
        void Error(string error);
        void CallQueryFromUser();
        void Menu();
    }
}
