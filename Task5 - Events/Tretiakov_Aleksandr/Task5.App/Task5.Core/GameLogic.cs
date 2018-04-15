using System;
using Task5.Core.Abstractions;
using Task5.Core.Models;

namespace BoardGame.Core
{
    public class AppLogic
    {
        readonly IMessenger _messenger;
        readonly IBoardHandler _boardHandler;
        readonly IProcessUserInput _processUserInput;
        public event EventHandler<UserChoise> Move; 

        public AppLogic(IMessenger messenger, IBoardHandler boardHandler, IProcessUserInput processUserInput)
        {
            _messenger = messenger ?? throw new ArgumentNullException();
            _boardHandler = boardHandler ?? throw new ArgumentNullException();
            _processUserInput = processUserInput ?? throw new ArgumentNullException();
        }

        public void Run()
        {
            _processUserInput.UserInput += OnUserInput;
            Move += _boardHandler.Hero.OnMove;
            _boardHandler.Hero.Moved += _boardHandler.Board.OnMove;
            foreach (var bomb in _boardHandler.Bombs)
            {
                bomb.BombExploded += OnBombExploded;
                _boardHandler.Hero.Moved += bomb.OnHeroMove;
            }
            _boardHandler.DrawAll();
            _processUserInput.StartListening();
        }

        private void OnUserInput(object sender, UserChoise e)
        {
            if (e == UserChoise.Restart)
            {
                _boardHandler.DrawAll();
                IsBombExploided = false;
                return;
            }

            if (IsBombExploided)
            {
                _messenger.ShowError("HIT");
                return;
            }

            if (!_boardHandler.IsValidMove(e))
            {
                return;
            }

            Move?.Invoke(this, e);
            _boardHandler.DrawAll();
        }

        public void OnBombExploded(object target, EventArgs args)
        {
            IsBombExploided = true;
        }

        private bool IsBombExploided { get; set; }
    }
}
