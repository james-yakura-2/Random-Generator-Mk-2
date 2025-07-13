using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Random_Elements.Processors.Aggregations.Math
{
    /// <summary>
    /// Adds up a set of numerical results.
    /// </summary>
    public class Sum : Summary<int,int>
    {
        /// <summary>
        /// Creates a new Sum.
        /// </summary>
        /// <param name="generators">The generator set </param>
        public Sum(Aggregation<int> generators):base(generators)
        {
        }

        public override int Process(int[] inputs)
        {
            int result = 0;
            foreach (int x in inputs)
                result += x;
            return result;
        }
    }
}
