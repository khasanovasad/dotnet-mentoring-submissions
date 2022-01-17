using System;
using System.Linq;

namespace Task2
{
    public class NumberParser : INumberParser
    {
        public int Parse(string stringValue)
        {
            if (stringValue is null)
            {
                throw new ArgumentNullException();
            }
            
            stringValue = stringValue.Trim();
            
            CheckForFormatException(stringValue);

            stringValue = TrimZeroesWhenTheLengthIsMoreThanOne(stringValue);

            bool isNegative = StringUtils.ContainsMinus(stringValue);
            bool isPositive = StringUtils.ContainsPlus(stringValue);

            if (isNegative || isPositive)
            {
                stringValue = stringValue.Remove(0, 1);
                
                stringValue = TrimZeroesWhenTheLengthIsMoreThanOne(stringValue);
            }
            
            Int32 result = 0;
            foreach (char ch in stringValue)
            {
                result *= 10;
                result += ch - '0';
            }

            if (isNegative)
            {
                result *= (-1);
            }

            stringValue = !stringValue.Equals("0") && isNegative ? stringValue.Insert(0, "-") : stringValue;
            
            CheckForOverflowException(stringValue, result, isNegative, isPositive);

            return result;
        }

        private static string TrimZeroesWhenTheLengthIsMoreThanOne(string stringValue)
        {
            if (stringValue.Length > 1)
            {
                stringValue = stringValue.TrimStart('0');
            }

            return stringValue;
        }

        private void CheckForOverflowException(string stringValue, int result, bool isNegative, bool isPositive)
        {
            if (!stringValue.Equals(result.ToString()))
            {
                throw new OverflowException();
            }
        }

        private void CheckForFormatException(string stringValue)
        {
            var functions = new Func<string, bool>[]
            {
                StringUtils.ContainsEmptyString,
                StringUtils.ContainsWhiteSpace,
                StringUtils.ContainsSymbol,
                StringUtils.ContainsAlphaChars,
                StringUtils.ContainsPlusAndMinus,
                StringUtils.ContainsDot,
                StringUtils.ContainsZeroMinusPlus
            };

            if (functions.Any(func => func(stringValue)))
            {
                throw new FormatException();
            }
        }
    }

    public static class StringUtils
    {
        public static bool ContainsEmptyString(string stringValue)
        {
            return stringValue.Equals(String.Empty);
        }
        
        public static bool ContainsWhiteSpace(string stringValue)
        {
            return stringValue.Contains(' ');
        }

        public static bool ContainsSymbol(string stringValue)
        {
            return stringValue.Contains(',') || stringValue.Contains('$');
        }

        public static bool ContainsAlphaChars(string stringValue)
        {
            return stringValue.Any(charValue => char.IsLetter(charValue));
        }

        public static bool ContainsPlusAndMinus(string stringValue)
        {
            return stringValue.Contains('-') && stringValue.Contains('+');
        }
        
        public static bool ContainsPlus(string stringValue)
        {
            return stringValue[0] == '+';
        }
        
        public static bool ContainsMinus(string stringValue)
        {
            return stringValue[0] == '-';
        }
        
        public static bool ContainsDot(string stringValue)
        {
            return stringValue.Contains('.');
        }
        
        public static bool ContainsZeroMinusPlus(string stringValue)
        {
            return stringValue.Contains("0-") || stringValue.Contains("0+");
        }
    }
}