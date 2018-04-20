using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM_homework_linq
{
    class Program
    {
        static void Main(string[] args)
        {
            IDataModel dataModel = new LinqToXmlProvider("..\\..\\XmlData.xml");

            dataModel.ShowOutput();

            Console.ReadKey();

            Console.Clear();

            using (var DbContext = new DbEfDataContext("EPAM_Linq"))
            {
                DbContext.ShowOutput();
            }
        }
    }
}
