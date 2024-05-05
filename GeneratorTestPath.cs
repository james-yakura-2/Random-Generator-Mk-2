using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Random_Elements.Generators;
using Random_Elements.Processors;

namespace Random_Generator_Mk_2
{
    internal class GeneratorTestPath<T> : MonteCarloPath<T[]>
    {
        List<T> path;
        int _depth;
        int currentDepth;
        Generator<T> _generator;

        public GeneratorTestPath(int depth, Generator<T> generator)
        {
            _depth = depth;
            _generator = generator;
            currentDepth = 0;
            path = new List<T>();
        }

        public override void iterate()
        {
            currentDepth++;
            if(_generator is Mutable<T>)
            {
                path.Add(((Mutable<T>)_generator).Pop());
            }
            else
            {
                path.Add(_generator.Peek());
            }
            if (currentDepth >= _depth)
                Done = true;
        }

        public override T[] output()
        {
            return path.ToArray();
        }
    }
}
