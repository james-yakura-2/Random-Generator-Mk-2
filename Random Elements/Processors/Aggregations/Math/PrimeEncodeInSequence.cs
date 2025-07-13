using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Random_Elements.Processors.Aggregations.Math
{
    /// <summary>
    /// Converts a set of integers to a single number that can be decoded in a way that preserves their order.
    /// Try to avoid using this if order is irrelevant; use <see cref="PrimeEncode{T}"/> instead in that case.
    /// </summary>
    public class PrimeEncodeInSequence: Summary<int, int>
    {
        public PrimeEncodeInSequence(Aggregation<int> inputs) : base(inputs)
        {
        }

        public override int Process(int[] inputs)
        {
            int multiplier = 1;
            int result = 1;
            foreach (int i in inputs)
            {
                multiplier=Utils.NextPrime(multiplier);
                result *= (int)System.Math.Pow(multiplier, i);
            }
            return result;
        }
    }
}
