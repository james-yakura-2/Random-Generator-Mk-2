using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomGeneratorCS.Randomizer
{
    /// <summary>
    /// Uses the C# Random class to generate random numbers.
    /// </summary>
    public class SystemRandom : Randomizer
    {
        Random rng;

        /// <summary>
        /// Creates a new SystemRandom
        /// </summary>
        /// <param name="name">A unique name for the SystemRandom.</param>
        public SystemRandom(string name)
        {
            Name = name;
            rng = new Random();
        }

        public override int Next(int min, int max)
        {
            return rng.Next(min, max);
        }
    }
}
