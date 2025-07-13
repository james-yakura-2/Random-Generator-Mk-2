using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Random_Elements.Processors.Aggregations.Math
{
    /// <summary>
    /// Generates a prime number that corresponds to the outputs of a set of inputs.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PrimeEncode<T> : Summary<int, T>
    {
        Dictionary<T, int> encodings;

        public PrimeEncode(Dictionary<T,int> encoding, Aggregation<T> inputs):base(inputs)
        {
            encodings = encoding;
        }

        public override int Process(T[] inputs)
        {
            int value = 1;
            foreach (T item in inputs)
            {
                if (!encodings.ContainsKey(item))
                {
                    lock (encodings)
                    {
                        int max = 0;
                        foreach (int x in encodings.Values)
                        {
                            if (x > max)
                            {
                                max = x;
                            }
                        }
                        encodings.Add(item, Math.Utils.NextPrime(max));
                    }

                }

                value *= encodings[item];
            }
            return value;
        }

    }

}
