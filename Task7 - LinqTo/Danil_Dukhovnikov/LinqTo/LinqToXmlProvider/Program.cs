using LinqTo.Core;

namespace LinqToXmlProvider
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            IDataModel dataModel = new XmlLinqDataModel(".\\data.xml");

            dataModel.ShowOutput();
        }
    }
}