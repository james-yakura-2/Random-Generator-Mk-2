using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Random_Elements.Randomizers
{
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
