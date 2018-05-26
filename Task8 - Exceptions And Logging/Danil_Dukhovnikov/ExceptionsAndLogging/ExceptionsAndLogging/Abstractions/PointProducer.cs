using System;

namespace ExceptionsAndLogging.Abstractions
{
    internal interface IPointProducer
    {
        bool IsContinue { get; set; }

        void Run(Action<IPoint> onPointRecieved);
    }

    internal abstract class PointProducer : IPointProducer
    {
        public bool IsContinue { get; set; }

        protected abstract IPoint BuildPoint(decimal x);

        protected abstract decimal WhereToStart { get; }

        protected abstract decimal GetNext(decimal x);

        public void Run(Action<IPoint> onPointRecived)
        {
            IsContinue = true;
            var x = this.WhereToStart;
            
            while (IsContinue)
            {
                var point = this.BuildPoint(x);
                onPointRecived?.Invoke(point);
                System.Threading.Thread.Sleep(1000);
                x = this.GetNext(x);
            }
        }
    }
}
