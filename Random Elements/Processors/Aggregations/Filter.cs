using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Random_Elements.Generators;

namespace Random_Elements.Processors.Aggregations
{
    /// <summary>
    /// Excludes certain results from a GeneratorSet.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Filter<T>:Aggregation<T>
    {

        public Aggregation<T> Inputs { get; private set; }
        public Filter(Aggregation<T> inputs)
        {
            Inputs = inputs;
        }

        /// <summary>
        /// Converts an array of results to an array lacking the results that should be excluded.
        /// </summary>
        /// <param name="inputs">The raw results from the underlying GeneratorSet.</param>
        /// <returns>The results to be passed to a higher level.</returns>
        public abstract T[] RunFilter(T[] inputs);

        public override T[] peekLogic()
        {
            return RunFilter(Inputs.Peek());
        }
    }
}
