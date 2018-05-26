using EntityCore.Model;
using System;

namespace EntityCore.Abstractions
{
   public  interface IProducer
    {
        bool IsContinue { get; set; }
        void Run(int number);
    }
}
