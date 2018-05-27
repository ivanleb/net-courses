using EF.Core.Abstractions;
using EF.Core.Services;

namespace EF.Core.Repositories
{
    public interface IBusinessRepository
    {
        IDataContext DataContext { get; }
        ILogService LogService { get; }
    }
}