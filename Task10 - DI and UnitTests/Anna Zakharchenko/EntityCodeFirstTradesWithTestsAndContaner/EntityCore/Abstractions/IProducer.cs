using EntityCore.Model;
using System;

namespace EntityCore.Abstractions
{
   public  interface IProducer
    {
        //public event EventHandler<Trade> OnBalanceChanged;
        bool IsContinue { get; set; }
        //Action<object, Trade> OnBalanceChanged { get; set; }

        void Run(int number);
    }
}
