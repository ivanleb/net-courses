using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extentions
{
    public static class CustomStringExtentions
    {
        public static String SetFirstLetterToUpperCase(this String StringToModify)
        {
            int firstLetterIndex = Array.FindIndex<char>(StringToModify.ToCharArray(),symbol => Char.IsLetter(symbol));
            if (firstLetterIndex < 0) return StringToModify;
            char firstLetter = StringToModify[firstLetterIndex];
            firstLetter = Char.ToUpper(firstLetter);
            return StringToModify.Remove(firstLetterIndex, 1).Insert(firstLetterIndex, firstLetter.ToString());
        } 
        public static String ApplyBraces(this String StringToModify)
        {
            return "{" + StringToModify + "}";
        }
        public static String ApplySpaces(this String StringToModify)
        {
            return " " + StringToModify + " ";
        }
        public static String AppendNumbers(this String StringToModify)
        {
            return StringToModify + " - 12345";
        }
        public static String IncludeCurrentYear(this String StringToModify)
        {
            return StringToModify +": "+ DateTime.Now.Year;
        }
        public enum MySmile { Laugh, Smile, Rofl}
        public static String AppendSmile(this String SadString, MySmile Smile)
        {
            string smile;
            switch (Smile)
            {
                case MySmile.Laugh:
                    smile = ":D"; break;
                case MySmile.Rofl:
                    smile = ":'D"; break;
                default:
                    smile = ":)"; break;
            }
            return smile + SadString;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string StringToDemostrate = Console.ReadLine();
            Console.WriteLine(StringToDemostrate
                .SetFirstLetterToUpperCase()
                .ApplySpaces()
                .ApplyBraces()
                .AppendNumbers()
                .IncludeCurrentYear()
                .AppendSmile(CustomStringExtentions.MySmile.Laugh)
                );
            Console.ReadKey(true);
        }
    }
}
