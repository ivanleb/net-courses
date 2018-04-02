using System;
using System.Collections.Generic;

namespace Delegates
{
    internal class UserIntaraction : IUserAction
    {
        public int GetInt(string description, int min = int.MinValue, int max = int.MaxValue)
        {
            var result = 0;
            var flag = true;
            while (flag || result < min || result > max)
            {
                Console.WriteLine(description);
                flag = !int.TryParse(Console.ReadLine(), out result);
            }
            return result;
        }

        public void ShowTypes(List<IBoard> dictionary)
        {
            var menu = string.Empty;
            var i = 1;
            foreach (var x in dictionary)
            {
                menu += i + " - " + x.GetName() + "\n";
                i++;
            }
            //menu = dictionary.Aggregate(menu, (current, x) => current + i + "-" + x.GetName() + "\n");
            Console.Write(menu);
        }
    }
}