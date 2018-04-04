using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataFile;

namespace T4ExampleCore.Abstractions
{
    public interface ICatalogManager
    {
        Catalog CreateNewCatalog();
        void FillMusicContent(Catalog catalog);
        void FillLiteratureContent(Catalog catalog);
    }
}
