using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Random_Elements.Randomizers;

namespace Random_Elements.Processors.Aggregations.Math
{
    /// <summary>
    /// Chooses a single item from a set of results at random.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RandomSelection<T> : Filter<T>
    {

        public Randomizer RNG { get; protected set; }

        public RandomSelection(Aggregation<T> inputs, Randomizer rng):base(inputs)
        {
            RNG = rng;
        }

        public override T[] RunFilter(T[] inputs)
        {
        
            int index=RNG.Next(inputs.Length);
            return new T[] { inputs[index] };       
        }
    }
}
