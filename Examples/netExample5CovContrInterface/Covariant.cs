using System;

namespace netExample5CovContrInterface.Covariant
{
    interface IBank<out T>  where T : Account
    {
        T DoOperation();
    }
 
    class Bank: IBank<DepositAccount>
    {
        public DepositAccount DoOperation()
        {
            DepositAccount acc = new DepositAccount();
            acc.DoTransfer();
            return acc;
        }
    }

    public static class Run
    {
        public static void Start()
        {
            IBank<DepositAccount> depositBank = new Bank();
            depositBank.DoOperation();
 
            IBank<Account> ordinaryBank = depositBank;
            ordinaryBank.DoOperation();
 
            Console.ReadLine();
        }
    }
}