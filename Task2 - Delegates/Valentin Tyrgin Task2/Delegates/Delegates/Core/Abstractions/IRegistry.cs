using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delegates.App.Implementations;
using Delegates.Core.Abstractions;

namespace Delegates
{
    public interface IRegistry
    {
        IBoard Board { get; set; }
        IUserAction User { get; set; }
        IUtils Utils { get; set; }
        ITextQuery Text { get; set; }
    }
}
