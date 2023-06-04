using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Random_Generator_Mk_2.Randomizers;

namespace Random_Generator_Mk_2.Generators
{
    public class Die<T>:Generator<T>
    {
        T[] _contents;
        Randomizer _rng;

        public Die(T[] contents, Randomizer rng)
        {
            _contents = contents;
            _rng = rng;
        }

        public override T peek()
        {
            return _contents[_rng.Next(_contents.Length-1)];
        }
    }
}
