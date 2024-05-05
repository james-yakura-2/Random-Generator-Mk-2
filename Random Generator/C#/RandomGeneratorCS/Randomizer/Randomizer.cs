using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomGeneratorCS.Randomizer
{
    /// <summary>
    /// Generates integers according to some rule.
    /// </summary>
    public abstract class Randomizer
    {
        /// <summary>
        /// Generates a number between the min and max values given.
        /// </summary>
        /// <param name="min">A number which the result is equal to or greater than.</param>
        /// <param name="max">A number which the result is less than.</param>
        /// <returns>A number between the specified limits.</returns>
        public abstract int Next(int min, int max);

        public string Name { get; set; }

        public virtual Dictionary<string, object> Serialize()
        {
            return new Dictionary<string, object>() { { "name", Name } };
        }
    }
}
