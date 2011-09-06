using System.Text;
using System.Text.RegularExpressions;

namespace NBTY.Core
{
    public static class StringExtensions
    {
        /// <summary>
        /// Transforms a Camel-Case variable into a word with proper spaces. For ex. "MyTestString" will return "My Test String"
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static string Wordify(this string inputString)
        {
            if (inputString == null) return null;
            //var newString = "";
            //var lastChar = 'a';
            //foreach (var character in inputString)
            //{
            //    newString += (char.IsUpper(character) && ! char.IsUpper(lastChar)) ? (" " + character) : character.ToString();
            //    lastChar = char.Parse(newString.Right(1));
            //}
            //return newString.FullTrim();

            var regExp = new Regex("(?<=[a-z])(?<x>[A-Z])|(?<=.)(?<x>[A-Z])(?=[a-z])");
            return regExp.Replace(inputString, " ${x}");
        }

        public static string ToCamelCase(this string inputString)
        {
            return ConvertToCase(inputString.Wordify(), Case.CamelCase);
        }

        public static string ToPascalCase(this string inputString)
        {
            return ConvertToCase(inputString.Wordify(), Case.PascalCase);
        }

        /// <summary>
        /// Return the characters to the left of the first occurrence of a specified substring, null if the substring is not found
        /// </summary>
        /// <param name="inputString"></param>
        /// <param name="substring"></param>
        /// <returns></returns>
        public static string Left(this string inputString, string substring)
        {
            if (inputString == null) return null;
            var pos = inputString.IndexOf(substring);
            if (inputString.IndexOf(substring) < 0) return string.Empty;
            return inputString.Substring(0, pos);
        }

        /// <summary>
        /// Return the characters to the left of the last occurrence of a specified substring, null if the substring is not found
        /// </summary>
        /// <param name="inputString"></param>
        /// <param name="substring"></param>
        /// <returns></returns>
        public static string LeftBack(this string inputString, string substring)
        {
            if (inputString == null) return null;
            var pos = inputString.LastIndexOf(substring);
            if (inputString.IndexOf(substring) < 0) return string.Empty;
            return inputString.Substring(0, pos);
        }

        /// <summary>
        /// Return the characters to the right of the first occurrence of a specified string, null if the substring is not found
        /// </summary>
        /// <param name="inputString"></param>
        /// <param name="substring"></param>
        /// <returns></returns>
        public static string Right(this string inputString, string substring)
        {
            if (inputString == null) return null;
            var pos = inputString.IndexOf(substring);
            if (inputString.IndexOf(substring) < 0) return string.Empty;
            return inputString.Substring(pos + substring.Length);
        }

        public static string Right(this string inputString, int numberOfChars)
        {
            if (inputString == null) return null;
            if (inputString.Length < numberOfChars) return string.Empty;

            return inputString.Substring(inputString.Length - numberOfChars);
        }

        /// <summary>
        /// Return the characters to the right of the last occurrence of a specified string, null if the substring is not found
        /// </summary>
        /// <param name="inputString"></param>
        /// <param name="substring"></param>
        /// <returns></returns>
        public static string RightBack(this string inputString, string substring)
        {
            if (inputString == null) return null;
            var pos = inputString.LastIndexOf(substring);
            if (inputString.IndexOf(substring) < 0) return string.Empty;
            return inputString.Substring(pos + substring.Length);
        }

        /// <summary>
        /// Return the characters between the first occurrence of a specified start string and the next occurrence of a specified end string, null if any of the substrings are not found
        /// </summary>
        /// <param name="inputString"></param>
        /// <param name="startString"></param>
        /// <param name="endString"></param>
        /// <returns></returns>
        public static string Substring(this string inputString, string startString, string endString)
        {
            if (inputString == null) return null;
            var tmpString = inputString.Right(startString);
            if (string.IsNullOrEmpty(tmpString)) return string.Empty;
            return tmpString.Left(endString);
        }

        /// <summary>
        /// Remove leading, trailing and redundant spaces from a string
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static string FullTrim(this string inputString)
        {
            if (inputString == null) return null;
            var trimmedInputString = inputString.Trim();
            return trimmedInputString.Replace("  ", " ");
        }

        /// <summary>
        /// Strip any special characters from the 
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static string StripSpecialChars(this string inputString)
        {
            if (inputString == null) return null;
            return Regex.Replace(inputString, @"[^\w\.@-]", "");
        }

        private static string ConvertToCase(string inputString, Case caseToConvertTo)
        {
            string[] splittedPhrase = inputString.Split(' ', '-', '.');
            var sb = new StringBuilder();

            if (caseToConvertTo == Case.CamelCase)
            {
                sb.Append(splittedPhrase[0].ToLower());
                splittedPhrase[0] = string.Empty;
            }
            else if (caseToConvertTo == Case.PascalCase)
                sb = new StringBuilder();

            foreach (var s in splittedPhrase)
            {
                char[] splittedPhraseChars = s.ToCharArray();
                if (splittedPhraseChars.Length > 0)
                {
                    splittedPhraseChars[0] = ((new string(splittedPhraseChars[0], 1)).ToUpper().ToCharArray())[0];
                }
                sb.Append(new string(splittedPhraseChars));
            }
            return sb.ToString();
        }

        enum Case
        {
            PascalCase,
            CamelCase
        }
    }
}