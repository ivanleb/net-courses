using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORMCore.Abstractions
{
    public interface IClient
    {
        int Id { get; set; }

        string Name { get; set; }

        string Surname { get; set; }

        string PhoneNumber { get; set; }

        decimal Balance { get; set; }

        string Area { get; set; }

        ICollection<IStock> Stocks { get; set; }
    }
}
