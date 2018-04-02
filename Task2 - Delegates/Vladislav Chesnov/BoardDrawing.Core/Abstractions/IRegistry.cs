using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardDrawing.Core.Abstractions
{
    public interface IRegistry
    {
        IBoard Board { get; set; }
        IProccesUserChoice ProccesUserChoice { get; set; }
        IShowMessageToUser ShowMessageToUser { get; set; }
    }
}
