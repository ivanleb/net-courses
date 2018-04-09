using System;

namespace netExample5CovContrInterface
{
    class Account
    {
        static Random rnd = new Random();

        public void DoTransfer()
        {
            int sum = rnd.Next(10, 120);
            Console.WriteLine($"Do transfer {sum}");
        }

        public string Name {get;set;}
    }

    class DepositAccount : Account
    {

    } 

    class Program
    {
        static void Main(string[] args)
        {
             netExample5CovContrInterface.Covariant.Run.Start();
             netExample5CovContrInterface.Contrvariant.Run.Start();
        }
    }
}
