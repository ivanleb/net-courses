using Delegates.Core.Abstractions;

namespace Delegates.App.Implementations
{
    internal class Registry:IRegistry
    {
        public IBoard Board { get; set; } = new Board();
        public IUserAction User { get; set; } = new UserIntaraction();
        public IUtils Utils { get; set; } = new Utils();
        public ITextQuery Text { get; set; } = new Text();
    }
}