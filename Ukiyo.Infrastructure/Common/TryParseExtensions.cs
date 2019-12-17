using System.Globalization;

namespace Ukiyo.Infrastructure.Common
{
    public static class TryParseExtensions
    {
        /// <summary>
        ///     Parses a string to a nullable int
        /// </summary>
        /// <param name="source">String to parse</param>
        /// <returns></returns>
        public static int? ParseIntOrNull(this string source)
        {
            if (int.TryParse(source, out var result))
            {
                return result;
            }

            return null;
        }

        /// <summary>
        ///     Parses a string to a nullable long
        /// </summary>
        /// <param name="source">String to parse</param>
        /// <returns></returns>
        public static long? ParseLongOrNull(this string source)
        {
            if (long.TryParse(source, out var result))
            {
                return result;
            }

            return null;
        }

        /// <summary>
        ///     Parses a string to a nullable double
        /// </summary>
        /// <param name="source">String to parse</param>
        /// <returns></returns>
        public static double? ParseDoubleOrNull(this string source)
        {
            if (double.TryParse(source.Replace(',', '.'), NumberStyles.Number, CultureInfo.InvariantCulture, out var result))
            {
                return result;
            }

            return null;
        }
    }
}