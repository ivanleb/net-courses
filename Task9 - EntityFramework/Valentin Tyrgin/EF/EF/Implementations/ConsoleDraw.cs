namespace EF.Implementations
{
    internal static class ConsoleDraw
    {
        public static string PrintInCentre(string str, int width = 12)
        {
            str = str ?? "";
            return str.PadRight(width - (width - str.Length) / 2).PadLeft(width);
        }
    }
}