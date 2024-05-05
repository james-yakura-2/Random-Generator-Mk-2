using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Random_Elements.Generators;

namespace Random_Elements.Processors
{
    /// <summary>
    /// Converts the output of a Generator to specific values based on a fixed mapping.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    public class Map<T, U> : Conversion<T,U> where U:notnull
    {
        Dictionary<U, T> _hashTable;

        public Map(Generator<U> baseGenerator, Dictionary<U, T> hashTable)
        {
            Input = baseGenerator;
            _hashTable = hashTable;
        }

        public override U Downprocess(T t)
        {
            foreach(U key in _hashTable.Keys)
            {
                T response=_hashTable[key];
                if (response == null & t == null)
                    return key;
                if (response != null && response.Equals(t))
                    return key;
            }
            throw new ArgumentOutOfRangeException();
        }

        public override T Process(U u)
        {
            return _hashTable[u];
        }
    }
}
