using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates.Core.Abstractions
{
    public interface IShowMessageToUser
    {
        void ShowMessage(string message);
        void ShowMenu();
    }
}
