using System.Threading.Tasks;
using DIAndUnitTests.ConsoleApp.Implementations;
using DIAndUnitTests.Core;
using DIAndUnitTests.Core.Abstractions;

namespace DIAndUnitTests.ConsoleApp.Refactoring
{
    public interface IAlphaDataContext
    {
        IDataContext DataContext { get; set; }
        IBusinessService BusinessService { get; set; }
        ISimulator Simulator { get; set; }
        
        Task Run();
    }
}