using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Random_Generator_Mk_2.Generators
{
    /// <summary>
    /// A class that returns a random element from a set of elements.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Generator<T>
    {
        /// <summary>
        /// Returns a random element from the generator. If the generator has mutable contents or sequence, this does not affect it.
        /// </summary>
        /// <returns>A random element from the generator.</returns>
        public abstract T peek();
    }
}
