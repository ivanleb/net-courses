using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeroesCore.Abstractions;

namespace HeroesCore
{
    public class Core
    {
        public void Run(IRegistery registery)
        {
            IBoard board = registery.Board;
            IModel model = registery.Model;
            IUserIteraction input = registery.UserIteraction;

            board.SetUpBoard(30, 15);
            board.Draw(model);

            model.Hero.StartListenInput(input);
         
            board.StartListenInput(input);
            input.StartListening();
        }
    }
}
