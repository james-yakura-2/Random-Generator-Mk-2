using Random_Elements.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Random_Elements.Processors.Aggregations.Math
{
    /// <summary>
    /// Converts a number into 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PrimeDecode<T> : Aggregation<T>
    {
        Dictionary<T, int> encodings;
        public Generator<int> generator { get; private set; }

        public PrimeDecode(Dictionary<T, int> encodings, Generator<int> generator)
        {
            this.encodings = encodings;
            this.generator = generator;
        }

        public override T[] peekLogic()
        {
            int input = generator.Peek();
            List<T> result = new List<T>();
            foreach(KeyValuePair<T, int> pair in encodings)
            {
                while (input % pair.Value == 0)
                {
                    result.Add(pair.Key);
                    input/=pair.Value;
                }
            }
            return result.ToArray();
        }
    }
}
