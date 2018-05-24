using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORMCore.Model;
using System.Threading;

namespace ORMCore.Abstractions
{
    public interface ITradingSimulator
    {
        bool IsContinue { get; set; }

        void FillDbWithData();
        void Run();
        void StopSequence(object sender, CommandEventArgs eventArgs);
    }
}
