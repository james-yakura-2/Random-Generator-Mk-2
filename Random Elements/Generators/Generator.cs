using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Random_Elements.Randomizers;

namespace Random_Elements.Generators
{
    /// <summary>
    /// A class that returns a random element from a set of elements.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Generator<T>
    {
        /// <summary>
        /// The most recent element generated, such as by <see cref="Peek()"/> or <see cref="Mutable{T}.Pop()"/>. Among other uses (such as allowing multiple processors to reference the same draw), this allows for easier verbose reporting.
        /// </summary>
        public T Previous { get; internal set; }

        public static explicit operator Generator<T>(Generator<object> v)
        {
            if (v is Generator<T>)
            {
                return v as Generator<T>;
            }
            else
                throw new ArgumentException("This does not generate " + typeof(T).AssemblyQualifiedName);
        }

        /// <summary>
        /// Returns a random element from the generator. If the generator has mutable contents or sequence, this does not affect it.
        /// </summary>
        /// <returns>A random element from the generator.</returns>
        public T Peek()
        {
            T result = peekLogic();
            Previous = result;
            return result;
        }

        /// <summary>
        /// The logic used to carry out the <see cref="Peek()"/> operation.
        /// </summary>
        /// <remarks>peekLogic() should never be called directly, since direct calls to peekLogic are not logged.</remarks>
        /// <returns>A random element from the generator.</returns>
        public abstract T peekLogic();
    }

    /// <summary>
    /// Creates a new Generator of the specified type.
    /// </summary>
    /// <typeparam name="T">The type of </typeparam>
    /// <returns></returns>
    public delegate Generator<T> factory<T>(Randomizer rng);
}
