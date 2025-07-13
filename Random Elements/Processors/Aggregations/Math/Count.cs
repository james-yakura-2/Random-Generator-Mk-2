using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Random_Elements.Processors.Aggregations.Math
{
    /// <summary>
    /// Returns the number of generators in a set.
    /// </summary>
    /// <typeparam name="T">The type of generators.</typeparam>
    public class Count<T>: Summary<int, T>
    {
        public Count(Aggregation<T> inputs):base(inputs)
        { }

        public override int Process(T[] inputs)
        {
            return inputs.Count();
        }
    }
}
