using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Random_Elements.Generators;

namespace Random_Elements.Processors.Aggregations
{
    /// <summary>
    /// Runs multiple generators.
    /// </summary>
    /// <typeparam name="T">The type of results to generate.</typeparam>
    public class MultiGenerator<T>:Aggregation<T>
    {
        public List<Generator<T>> Inputs { get; private set; }

        public MultiGenerator(Generator<T>[] inputs)
        {
            Inputs = new List<Generator<T>>(inputs);
        }

        public override T[] peekLogic()
        {
            T[] result = new T[Inputs.Count];
            for (int i = 0; i < result.Length; i++)
                result[i] = Inputs[i].Peek();
            return result;
        }

       
    }
}
