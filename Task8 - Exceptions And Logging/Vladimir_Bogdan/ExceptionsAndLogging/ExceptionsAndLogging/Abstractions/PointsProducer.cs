using System;

namespace ExceptionsAndLogging.Abstractions
{
    interface IPointProducer
    {
        bool IsContinue { get; set; }

        void Run(Action<IPoint> onPointRecieved);
    }

    abstract class PointsProducer : IPointProducer
    {
        public bool IsContinue { get; set; }

        protected abstract IPoint BuildPoint(decimal x);

        protected abstract decimal WhereToStart { get; }

        protected abstract decimal GetNext(decimal x);

        public void Run(Action<IPoint> onPointRecived)
        {
            IsContinue = true;
            decimal x = this.WhereToStart;
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
