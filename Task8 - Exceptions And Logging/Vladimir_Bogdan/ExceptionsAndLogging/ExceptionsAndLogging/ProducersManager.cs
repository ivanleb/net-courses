using ExceptionsAndLogging.Abstractions;
using System;
using System.Collections.Generic;

namespace ExceptionsAndLogging
{
    class ProducerCancelingManager
    {
        private Dictionary<char, Action> cancelers = new Dictionary<char, Action>();
        public void Add(IPointProducer producer, char cancelKey)
        {
            cancelers.Add(cancelKey, () => { producer.IsContinue = false; });
        }
        public void CancelProducer(ConsoleKeyInfo key)
        {
            Action cancelAction;
            if (cancelers.TryGetValue(key.KeyChar, out cancelAction))
            {
                cancelAction.Invoke();
            }

        }
    }
}
