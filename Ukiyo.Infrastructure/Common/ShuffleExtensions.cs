using System;
using System.Collections.Generic;

namespace Ukiyo.Infrastructure.Common
{
    /// <summary>
    ///     Provides <c>Shuffle</c> extensions methods for <see cref="IList{T}" />.
    /// </summary>
    public static class ShuffleExtensions
    {
        private static readonly Random Rng = new Random();

        /// <summary>
        ///     Shuffles the items in a list.
        /// </summary>
        /// <param name="list">The list that should get shuffled.</param>
        /// <typeparam name="T">The type.</typeparam>
        public static void Shuffle<T>(this IList<T> list)
        {
            var n = list.Count;
            while (n > 1)
            {
                n--;
                var k = Rng.Next(n + 1);
                var value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}