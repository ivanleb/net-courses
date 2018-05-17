using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Core;


namespace LinqToXmlProvider
{
    class Program
    {
        static void Main(string[] args)
        {
            IDataModel xmlData = new XmlLinqDataModel("data.xml");
            xmlData.ShowData();
        }
    }
}
