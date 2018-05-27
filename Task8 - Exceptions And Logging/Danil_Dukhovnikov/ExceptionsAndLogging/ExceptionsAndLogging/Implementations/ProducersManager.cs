using System;
using System.Collections.Generic;
using ExceptionsAndLogging.Abstractions;

namespace ExceptionsAndLogging.Implementations
{
    internal class ProducerCancelingManager
    {
        private readonly Dictionary<char, Action> _cancelers = new Dictionary<char, Action>();
        public void Add(IPointProducer producer, char cancelKey)
        {
            _cancelers.Add(cancelKey, () => { producer.IsContinue = false; });
        }
        public void CancelProducer(ConsoleKeyInfo key)
        {
            if (_cancelers.TryGetValue(key.KeyChar, out var cancelAction))
            {
                cancelAction.Invoke();
            }

        }
    }
}
