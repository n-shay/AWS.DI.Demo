namespace TRG.Extensions
{
    using System.Collections.Generic;
    using System.Linq;

    public static class StringExtensions
    {
        /// <summary>
        /// If provided string has non white space characters, retrieves trimmed string. Otherwise, returns <value>null</value>.
        /// </summary>
        public static string SafeTrim(this string str)
        {
            return str.SafeTrim(string.Empty);
        }

        /// <summary>
        /// If provided string has non white space characters, retrieves trimmed string. Otherwise, returns <value>null</value>.
        /// </summary>
        public static string SafeTrim(this string str, string defaultValue)
        {
            return str == null || str == defaultValue
                ? null
                : str.Trim();
        }

        /// <summary>
        /// Concatenates the collection of strings with <paramref name="separator"/> between them.
        /// <para>Method will ignore any null or whitespace values.</para>
        /// </summary>
        public static string Concatenate(this IEnumerable<string> collection, string separator)
        {
            return collection == null 
                ? null 
                : string.Join(
                    separator ?? string.Empty, 
                    collection.Where(s => !string.IsNullOrWhiteSpace(s)));
        }
    }
}