using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Random_Elements.Randomizers
{
    public abstract class Randomizer
    {
        /// <summary>
        /// Generates a random or pseudo-random number.
        /// </summary>
        /// <param name="maximum">The highest number that can be generated.</param>
        /// <returns>A number between 0 and the maximum.</returns>
        public abstract int Next(int maximum);
    }
}
