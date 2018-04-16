using System;
using System.Windows.Forms;
using Events.Core;

namespace Events.App.Implementations
{
    internal class User : IUserAction
    {
        private UserInputEventArgs args { get; set; }
        public event UserInputKeyEvent InputNotification;

        public void StartNotification()
        {
            args = new UserInputEventArgs();
            while (true)
            {
                args.Key = Console.ReadKey().Key;
                if (args.Key == ConsoleKey.Escape) break;
                InputNotification?.Invoke(this, args);
            }
        }

        public void OnExplosion(object sender, EventArgs args)
        {
            MessageBox.Show("Game Over");
        }
    }
}