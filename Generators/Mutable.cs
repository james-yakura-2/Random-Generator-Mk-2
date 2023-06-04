using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Random_Generator_Mk_2.Generators
{
    /// <summary>
    /// Indicates that a Generator can have elements added and removed.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface Mutable<T>
    {
        /// <summary>
        /// Return an item from the Generator and remove it.
        /// </summary>
        /// <returns>A randomly selected item.</returns>
        public T pop();

        /// <summary>
        /// Add an item to the Generator.
        /// </summary>
        /// <param name="value">The item to be added.</param>
        public void push(T value);

    }
}
