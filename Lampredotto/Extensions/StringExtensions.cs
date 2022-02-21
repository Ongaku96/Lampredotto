using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lampredotto.Extensions
{
    static class StringExtensions
    {
        public static string ToDBString(this string value)
        {
            return "'" + value.Replace("'", "''") + "'";
        }
        public static string ToHTML(this string value)
        {
            return value.Replace(Environment.NewLine, "<br />");
        }

        public static int IndexOfFirstNumber(this string value)
        {
            for (int i = 0; i <= value.Length - 1; i++)
            {
                if (char.IsNumber(value[i])) return i;
            }
            return 0;
        }
        public static int IndexOfFirstLetter(this string value)
        {
            for (int i = 0; i <= value.Length - 1; i++)
            {
                if (char.IsLetter(value[i]))
                    return i;
            }

            return 0;
        }
        public static bool IsNumeric(this string s)
        {
            float output;
            return float.TryParse(s, out output);
        }
        public static string Right(this string value, int length)
        {
            return value.Substring(value.Length - length, length);
        }
        public static string Left(this string value, int length)
        {
            return value.Substring(0, length);
        }
        public static string GetOnlyUppercase(this string value)
        {
            var _upper = "";
            var _array = value.ToCharArray();
            foreach (var _char in _array)
            {
                if (char.IsUpper(_char))
                    _upper += _char;
            }
            return _upper;
        }
    }
}
