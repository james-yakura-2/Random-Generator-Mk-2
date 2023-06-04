using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Random_Generator_Mk_2.Randomizers
{
    class SystemRandom:Randomizer
    {
        Random rng;

        public SystemRandom() { rng=new Random(); }

        public override int Next(int maximum)
        {
            return rng.Next(maximum + 1);
        }
    }
}
