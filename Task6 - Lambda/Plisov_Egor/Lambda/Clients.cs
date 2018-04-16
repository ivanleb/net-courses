using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lambda
{
    class FirstClient : IClient
    {
        public void onNumReceived(int num)
        {
            Console.WriteLine(num + " - First client");
        }
    }

    class SecondClient : IClient
    {
        public void onNumReceived(int num)
        {
            Console.WriteLine(num + " - Second client");
        }
    }
}
