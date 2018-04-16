using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardDrawing.Core.Abstractions
{
    public interface IUserInteraction
    {
        void StartListening(IModel model, IBoard board);
        //void StartListeningMine();
        event HeroMovesHandler InputRecieved;
        event ExplosionHandler HeroStepsOnMine;
    }

}
