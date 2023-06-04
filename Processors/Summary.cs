using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Random_Generator_Mk_2.Generators;

namespace Random_Generator_Mk_2.Processors
{
    /// <summary>
    /// Finds some summary parameter of a set of results.
    /// </summary>
    /// <typeparam name="T">The type of the summary parameter.</typeparam>
    /// <typeparam name="U"></typeparam>
    public abstract class Summary<T,U>:Generator<T>
    {
        public GeneratorSet<U> Inputs { get; set; }

        public abstract T Process(U[] inputs);

        public override T peek()
        {
            return Process(Inputs.peek());
        }
    }
}
