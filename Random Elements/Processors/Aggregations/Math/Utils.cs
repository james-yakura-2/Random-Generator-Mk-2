using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Random_Elements.Processors.Aggregations.Math
{
    /// <summary>
    /// Utility functions for math aggregations.
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// Checks whether a number is prime.
        /// </summary>
        /// <param name="value">The number to check.</param>
        /// <returns>Whether the number is prime.</returns>
        public static bool IsPrime(int value)
        {
            if (value < 2)
            {
                return false;
            }
            int end = (int)System.Math.Sqrt(value);
            for (int i = 2; i <= end; i++)
            {
                if (value % i == 0)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Finds the next prime number after a given number.
        /// </summary>
        /// <param name="value">The number to find the next prime after.</param>
        /// <returns>The next prime after the specified number.</returns>
        public static int NextPrime(int value)
        {
            do
            {
                value++;
            } while (!IsPrime(value));
            return value;
        }

        /// <summary>
        /// Generates a dictionary mapping unique objects to prime numbers.
        /// </summary>
        /// <typeparam name="T">The type of items to map.</typeparam>
        /// <param name="items">The objects to be mapped. Unexpected behavior may occur if an object is not included exactly once.</param>
        /// <returns></returns>
        public static Dictionary<T,int> generatePrimeMap<T>(T[] items)
        {
            Dictionary<T, int> encodings = new Dictionary<T, int>();
            int value = 1;
            foreach (T item in items)
            {
                value=NextPrime(value);
                encodings.Add(item, value);
            }
            return encodings;
        }

    }
}
