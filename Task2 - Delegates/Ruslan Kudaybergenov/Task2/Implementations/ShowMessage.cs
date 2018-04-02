using System;
using Task2.Abstractions;

namespace Task2.Implementations
{
    public class ShowMessage : IShowMessageToUser
    {
        public void CallQueryFromUser()
        {
            Console.WriteLine("Please, choose an option");
        }

        public void Error(string error)
        {
            Console.WriteLine("Exception!"+error);
        }

        public void Message(string message)
        {
            Console.WriteLine(message);
        }
        public void Menu()
        {
            this.Message("[1]dot\n[2]Horizontal line\n[3]Vertical line\n[4]Horizontal+Vertical lines\n[0]exit");
        }
        
    }
}
