using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace EPAM_homework_lambdas
{
    interface Client
    {
        void onNumberReceived(int x);
    }

    public class BlueClient : Client
    {
        public void onNumberReceived(int x)
        {
            ForegroundColor = ConsoleColor.Blue;
            Write(x + " ");
            ForegroundColor = ConsoleColor.White;
        }
    }

    public class RedClient : Client
    {
        public void onNumberReceived(int x)
        {
            ForegroundColor = ConsoleColor.Red;
            Write(x + " ");
            ForegroundColor = ConsoleColor.White;
        }
    }

    public class GreenClient : Client
    {
        public void onNumberReceived(int x)
        {
            ForegroundColor = ConsoleColor.Green;
            Write(x + " ");
            ForegroundColor = ConsoleColor.White;
        }
    }
}
