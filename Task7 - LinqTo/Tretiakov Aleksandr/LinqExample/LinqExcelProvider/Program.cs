using LinqExampleCore;
using LinqToExcel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqExcelProvider
{
    class ExcelLinqDataModel : IDataModel
    {
        private readonly ExcelQueryFactory dataProvider;

        public ExcelLinqDataModel(string pathToDataFile)
        {
            this.dataProvider = new ExcelQueryFactory(pathToDataFile);
        }

        public IQueryable<Book> Books
        {
            get
            {
                return dataProvider.Worksheet<Book>();
            }
        }
    }


    class Program
    {
       
        static void Main(string[] args)
        {
            IDataModel dataModel = new ExcelLinqDataModel(".\\data.xlsx");

            dataModel.ShowOutput();
        }
    }
}
