using System;
using Task2.Abstractions;

namespace Task2.Implementations
{
    class Input : IUserInputs
    {
        public int SettedOperation
        {
            get { return GetNumOfOperation(); }
        }

        private int GetNumOfOperation()
        {
            ShowMessage show = new ShowMessage();
            show.Menu();
            while (true)
                try
                {
                    return Int32.Parse(Console.ReadLine());
                }
                catch (FormatException e)
                {
                    show.Message(e.Message);
                }
        }
    }
}
