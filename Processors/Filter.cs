using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Random_Generator_Mk_2.Processors
{
    /// <summary>
    /// Excludes certain results from a GeneratorSet.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Filter<T>:GeneratorSet<T>
    {
        public Filter(GeneratorSet<T> inputs) : base(inputs.Inputs)
            { }

        /// <summary>
        /// Converts an array of results to an array lacking the results that should be excluded.
        /// </summary>
        /// <param name="inputs">The raw results from the underlying GeneratorSet.</param>
        /// <returns>The results to be passed to a higher level.</returns>
        public abstract T[] RunFilter(T[] inputs);

        public override T[] peek()
        {
            return RunFilter(base.peek());
        }

        public override T[] pop()
        {
            return RunFilter(base.pop());
        }

    }
}
