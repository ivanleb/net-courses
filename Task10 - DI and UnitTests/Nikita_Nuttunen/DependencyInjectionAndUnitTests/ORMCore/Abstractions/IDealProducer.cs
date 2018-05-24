using ORMCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORMCore.Abstractions
{
    public interface IDealProducer
    {
        bool IsContinue { get; set; }
        void Run();
    }    
}
