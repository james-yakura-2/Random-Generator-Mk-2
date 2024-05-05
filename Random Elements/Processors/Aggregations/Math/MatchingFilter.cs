using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Random_Elements.Processors.Aggregations.Math
{
    /// <summary>
    /// A filter that includes only those elements contained in a particular aggregation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MatchingFilter<T>:ResultWiseFilter<T>
    {
        Aggregation<T> matches;
        T[] _values;

        public MatchingFilter(Aggregation<T> inputs, T[] values):base(inputs)
        {
            matches = inputs;
            _values = values;
        }
        
        public override T[] peekLogic()
        {
            return base.peekLogic();
        }

        public override bool Include(T obj)
        {
            foreach (T x in _values)
                if (obj.Equals(x))
                    return true;
            return false;
        }
    }
}
