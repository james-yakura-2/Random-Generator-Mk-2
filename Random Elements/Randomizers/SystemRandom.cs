using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Random_Elements.Randomizers
{
    /// <summary>
    /// Uses the default system random number generator.
    /// </summary>
    public class SystemRandom:Randomizer
    {
        Random rng;

        public SystemRandom() { rng=new Random(); }

        public override int Next(int maximum)
        {
            return rng.Next(maximum);
        }
    }
}
