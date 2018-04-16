using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events
{
    class Mover : IMover
    {
        public int Xpos { get; set; }
        public int Ypos { get; set; }

        public event EventHandler<EventArgs> MoverTurn;
        public event EventHandler<EventArgs> Move;

        public void startListenInput(IUserInput input)
        {
            input.InputReceived += OnInputReceived;
        }

        private void OnInputReceived(object sender, EventArgs e)
        {
            var args = (TurnEventArgs)e;

            if ((args.ReceivedCommand == System.ConsoleKey.LeftArrow) && (this.Xpos > 1))
            {
                this.Xpos--;
                MoverTurn?.Invoke(this, new MoverTurnEventArgs { NewX = Xpos, NewY = Ypos });
            }
            if ((args.ReceivedCommand == System.ConsoleKey.RightArrow) && (this.Xpos < StaticRegistry.board.Width - 1))
            {
                this.Xpos++;
                MoverTurn?.Invoke(this, new MoverTurnEventArgs { NewX = Xpos, NewY = Ypos });
            }
            if ((args.ReceivedCommand == System.ConsoleKey.UpArrow) && (this.Ypos > 1))
            {
                this.Ypos--;
                MoverTurn?.Invoke(this, new MoverTurnEventArgs { NewX = Xpos, NewY = Ypos });
            }
            if ((args.ReceivedCommand == System.ConsoleKey.DownArrow) && (this.Ypos < StaticRegistry.board.Height - 1))
            {
                this.Ypos++;
                MoverTurn?.Invoke(this, new MoverTurnEventArgs { NewX = Xpos, NewY = Ypos });
            }
        }
    }
}
