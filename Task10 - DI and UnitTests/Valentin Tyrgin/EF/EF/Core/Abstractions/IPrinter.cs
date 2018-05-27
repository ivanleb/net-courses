using System.Linq;
using EF.Core.Services;

namespace EF.Core.Abstractions
{
    internal interface IPrinter
    {
        void ShowAll(IQueryable<IEntityService> entities, string header);
        void ShowMessage(string message);
    }
}