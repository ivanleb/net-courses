using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM_homework_events
{
    class ConsoleInterface : IUserInterface
    {
        Dictionary<string, ConsoleColor> Logs = new Dictionary<string, ConsoleColor>();

        public void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void BombDestroyedInfo(TimeSpan time)
        {
            if (Logs.Count > 5)
                Logs.Clear();

            Logs.Add(time.ToString() + " BOOM!!!", ConsoleColor.DarkRed);
        }

        public void DisplayLogs()
        {
            foreach (KeyValuePair<string, ConsoleColor> pair in Logs)
            {
                Console.ForegroundColor = pair.Value;
                ShowMessage(pair.Key);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}
