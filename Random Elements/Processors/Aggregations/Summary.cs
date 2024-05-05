using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Random_Elements.Generators;
using Random_Elements.Processors.Aggregations;

namespace Random_Elements.Processors
{
    /// <summary>
    /// Finds some summary parameter of a set of results.
    /// </summary>
    /// <typeparam name="T">The type of the summary parameter.</typeparam>
    /// <typeparam name="U"></typeparam>
    public abstract class Summary<T,U>:Generator<T>
    {
        public Aggregation<U> Inputs { get; set; }

        public Summary(Aggregation<U> inputs) { Inputs = inputs; }

        public abstract T Process(U[] inputs);

        public override T peekLogic()
        {
            return Process(Inputs.Peek());
        }
    }
}
