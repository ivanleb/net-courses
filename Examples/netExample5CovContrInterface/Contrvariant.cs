using System;

namespace netExample5CovContrInterface.Contrvariant
{
    interface IBank<in T> where T: Account
    {
        void GetStatistic(T account);
    }

    class Bank<T> : IBank<T> where T : Account
    {
        public void GetStatistic(T account)
        {
            Console.WriteLine(account.Name);
        }
    }

    public static class Run
    {
        public static void Start()
        {
            Account account = new Account() { Name = "simple account" };
            IBank<Account> ordinaryBank = new Bank<Account>();
            ordinaryBank.GetStatistic(account);
 
            DepositAccount depositAcc = new DepositAccount() { Name = "deposit account" };
            IBank<DepositAccount> depositBank = ordinaryBank;
            depositBank.GetStatistic(depositAcc);
 
            Console.ReadLine();
        }
    }
}