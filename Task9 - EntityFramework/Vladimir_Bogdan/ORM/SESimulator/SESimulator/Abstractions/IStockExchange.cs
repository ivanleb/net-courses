using System;

namespace SESimulator.Abstractions
{
    public interface IStockExchange
    {
        bool IsContinue { get; set; }
        void Run(Action<IDealInfo> onDealRecived);
    }
    public abstract class StockExchange : IStockExchange
    {
        public bool IsContinue { get; set; }

        protected abstract IDealInfo MakeDeal();

        public void Run(Action<IDealInfo> onDealRecived)
        {
            this.IsContinue = true;
            while (this.IsContinue)
            {
                IDealInfo dealInfo = this.MakeDeal();
                onDealRecived?.Invoke(dealInfo);
                System.Threading.Thread.Sleep(5 * 1000);
            }
        }
    }
}
