using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EF.Implementations.Entities;

namespace EF.Implementations
{
    public class Status : IEnumerable
    {
        private static Status Instance;

        private Status()
        {
            StatusCollection = new List<ParticularStatus>();
            Initialize();
        }

        private List<ParticularStatus> StatusCollection { get; }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return StatusCollection.GetEnumerator();
        }

        public static Status GetStatusInstance()
        {
            return Instance ?? (Instance = new Status());
        }

        public bool Add(string title, Predicate<decimal> filter)
        {
            if (StatusCollection.Select(x => x.Title).Contains(title))
                return false;
            StatusCollection.Add(new ParticularStatus(title, filter));
            return true;
        }

        public bool Remove(string title)
        {
            if (!StatusCollection.Select(x => x.Title).Contains(title)) return false;
            StatusCollection.Remove(StatusCollection.First(x => x.Title == title));
            return true;
        }

        public string ApplyStatus(Trader trader)
        {
            return StatusCollection.First(x => x.Filter(trader.Balance)).Title;
        }

        public void ShowAvailableStatuses()
        {
            foreach (var paticularStatuse in StatusCollection)
                Console.WriteLine($"{paticularStatuse.Title} | {paticularStatuse.Filter}");
        }

        private void Initialize()
        {
            Add("Orange", x => x == 0);
            Add("Black", x => x < 0);
            Add("White", x => x > 0);
        }

        private class ParticularStatus
        {
            internal ParticularStatus(string title, Predicate<decimal> filter)
            {
                Title = title;
                Filter = filter;
            }

            internal string Title { get; }
            internal Predicate<decimal> Filter { get; }
        }
    }
}