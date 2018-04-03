using Delegates.Core.Abstractions;

namespace Delegates.App.Implementations
{
    class Text:ITextQuery
    {
        public string IntQuery { get; set; } = "Введите номер";
        
        public string SelectionQuery {get; set; } ="Выберите объект для построения\n";
    }
}
