using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task6
{
    class NumberGenerator
    {
        private Action<int> OnNumberReceived { get; set; }
        private Func<int, bool> UseFilter { get; set; }

        private readonly Dictionary<Action<int>, Func<int, bool>> _simpleSubscribers;
        private readonly Dictionary<Action<int>, IEnumerable<Func<int, bool>>> _complicatedSubscribers;

        public NumberGenerator()
        {
            _simpleSubscribers = new Dictionary<Action<int>, Func<int, bool>>();
            _complicatedSubscribers = new Dictionary<Action<int>, IEnumerable<Func<int, bool>>>();
        }

        public List<int> Generate(int amount, int min = 0, int max = 0)
        {
            var random = new Random();
            var result = new List<int>();
            if (min > max)
                throw new ArgumentException("Max must be greater than min");
            if (amount < 0)
                throw new ArgumentException("Amount must be non-negative");
            for (int i = 0; i < amount; i++)
            {
                var value = random.Next(min, max);
                NotifySimpleSubscribers(value);
                NotifyComplicatedSubscribers(value);
                result.Add(value);
            }
            return result;
        }

        private void NotifyComplicatedSubscribers(int value)
        {
            foreach (var subscriber in _complicatedSubscribers)
            {
                var isValid = true;
                foreach (var filter in subscriber.Value)
                {
                    if (filter(value) == false)
                    {
                        isValid = false;
                        break;
                    }
                }
                if (isValid)
                {
                    subscriber.Key(value);
                }
            }
        }

        private void NotifySimpleSubscribers(int value)
        {
            foreach (var subscriber in _simpleSubscribers)
            {
                if (subscriber.Value(value))
                {
                    subscriber.Key(value);
                }
            }
        }

        public void Subscribe(Action<int> onNumberReceived, Func<int, bool> useFilter)
        {
            if (_simpleSubscribers.ContainsKey(onNumberReceived))
                return;
            _simpleSubscribers[onNumberReceived] = useFilter;
        }

        public void Subscribe(Action<int> onNumberReceived, IEnumerable<Func<int, bool>> useFilter)
        {
            if (_complicatedSubscribers.ContainsKey(onNumberReceived))
                return;
            _complicatedSubscribers[onNumberReceived] = useFilter;
        }
    }

    class Client
    {
        private readonly string _name;
        public Client(string name)
        {
            _name = name ?? throw new ArgumentNullException("Name must be not null");
        }
        public void HandleEvent(int x)
        {
            Console.WriteLine($"{_name}: {x}");
        }
    }
}
