using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rand = RandomGeneratorCS.Randomizer.Randomizer;

namespace RandomGeneratorCS.Generators.Simple
{
    /// <summary>
    /// A class for generating random items.
    /// </summary>
    /// <typeparam name="T">The type of items to be generated.</typeparam>
    public abstract class Generator<T> where T:notnull
    {
        /// <summary>
        /// Generates the next item.
        /// </summary>
        /// <returns>A random item of type T.</returns>
        public abstract T Peek();

        public string Name { get; set; }

        public string Type { get; set; }

        public Rand RNG { get; set; }

        /// <summary>
        /// Converts the object to a Dictionary containing all attributes needed to create it.
        /// </summary>
        /// <returns>A Dictionary containing all data needed to re-create the object in its current state.</returns>
        public virtual Dictionary<string, object> Serialize()
        {
            return new Dictionary<string, object>() { { "name", Name }, { "type", Type }, { "rng", RNG } };
        }

        /// <summary>
        /// The number of items available for the next Peek().
        /// </summary>
        public abstract int Length { get; }
    }
}
