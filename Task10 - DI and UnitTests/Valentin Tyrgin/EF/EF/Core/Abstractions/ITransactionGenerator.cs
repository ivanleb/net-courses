using EF.Core.Repositories;
using EF.Core.Services;

namespace EF.Core.Abstractions
{
    internal interface ITransactionGenerator : ITransactionGeneratorRepository, ITransactionGeneratorService
    {
        
    }
}