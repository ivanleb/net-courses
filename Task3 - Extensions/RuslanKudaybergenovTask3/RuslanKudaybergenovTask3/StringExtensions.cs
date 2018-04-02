using System;
using System.Collections.Generic;

namespace Extensions
{
    public static class StringExtensions
    {
        static public string AppendNumbers(this string str, List<int> numbers)
        {
            foreach (var number in numbers)
                str += number;
            return str;
        }

        static public string AppendSmile(this string str)
        {
            return str + "C:";
        }

        static public string ApplyBraces(this string str, string frontBracer, string backBracer)
        {
            return frontBracer + str + backBracer;
        }

        static public string ApplySpaces(this string str, int startIndex,int numOfSpaces)
        {
            if (numOfSpaces <= 0)
                throw new ArgumentException();
            for (int i = 0; i < numOfSpaces; i++)
                str=str.Insert(startIndex, " ");
            return str;
        }

        static public string IncludeCurrentYear(this string str)
        {
            return str + DateTime.Now.Year.ToString();
        }

        static public string SetFirstLetterToUpperCase(this string str)
        {
            for (int i = 0; i < str.Length; i++)
            { 
                if((str[i]>='a'&&str[i]<='z')||(str[i]>='A'&&str[i]<='Z'))
                {
                    return str.Remove(i, 1).Insert(i, str[i].ToString().ToUpper());
                }
            }
            return str;
        }
    }
}
