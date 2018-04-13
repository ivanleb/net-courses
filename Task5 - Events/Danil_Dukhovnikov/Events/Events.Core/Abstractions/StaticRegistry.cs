namespace Events.Core.Abstractions
{
    public static class StaticRegistry
    {
        public static IBoard Board { get; set; }
        public static IUserInteraction UserInteraction { get; set; }
        public static IModel Model { get; set; } 
    }
}