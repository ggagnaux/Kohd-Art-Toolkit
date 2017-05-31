using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KohdAndArt.Toolkit.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveLast(this string s, int numberOfCharsToRemove)
        {
            string result = string.Empty;
            if (s.Length == 0)
            {
                throw new ArgumentException("Input string is empty.");
            }
            if (s.Length < numberOfCharsToRemove)
            {
                throw new ArgumentException("Attempting to remove more characters than are available to remove");
            }

            result = s.Remove(s.Length - numberOfCharsToRemove, numberOfCharsToRemove);

            return result;
        }
    }
}
