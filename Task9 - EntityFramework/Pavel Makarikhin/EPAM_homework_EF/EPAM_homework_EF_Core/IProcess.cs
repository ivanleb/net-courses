using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM_homework_EF_Core
{
    public interface IProcess
    {
        void Run(BussinesService service);
    }
}
