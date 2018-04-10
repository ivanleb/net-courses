using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsCore.Abstractions
{
    public interface IBoard
    {
        int Width { get; set; }
        int Height { get; set; }
        void Initialize();
        void Draw(IModel model);
        void ListenToTheInput(IUserInput input);
    }
}
