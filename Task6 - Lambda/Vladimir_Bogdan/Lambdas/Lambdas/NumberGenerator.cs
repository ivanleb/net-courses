using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Lambdas
{
    class NumberGenerator
    {
        private class Subscriber
        {
            private Action<int> onNumberReceived;
            private IEnumerable<Func<int, bool>> useFilters;
            public Subscriber(Action<int> OnNumberReceived, IEnumerable<Func<int, bool>> UseFilters)
            {
                this.onNumberReceived = OnNumberReceived;
                this.useFilters = UseFilters;
            }
            private bool SatisfiesTheFilters (int number)
            {
                if (this.useFilters == null) return true;
                foreach (var filterItem in useFilters)
                {
                    if (!filterItem(number)) return false;
                }
                return true;
            }
            public void GetNotification (int number)
            {
                if ((onNumberReceived != null) && (SatisfiesTheFilters(number)))
                {
                    onNumberReceived(number);
                }
            }
        }
        private List<Subscriber> Subscribers = new List<Subscriber>();
        private delegate void Notification(int number);
        Notification Notify = null;
        public void Subscribe(Action<int> onNumberReceived, IEnumerable<Func<int, bool>> useFilter)
        {
            var subscriber = new Subscriber(onNumberReceived, useFilter);
            Subscribers.Add(subscriber);
            Notify += subscriber.GetNotification;
        }
        public void Generate(int count)
        {
            for (int i=0; i<count; i++)
            {
                if (Notify!= null) Notify(i);
            }
        }
    }
}
