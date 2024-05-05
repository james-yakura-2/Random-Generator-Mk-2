using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Random_Elements.Processors.Aggregations.Math
{
    /// <summary>
    /// Gets the maximum value of a group of numerical inputs.
    /// </summary>
    public class Highest : Summary<int, int>
    {
        /// <summary>
        /// Creates a new Highest parameter.
        /// </summary>
        /// <param name="inputs">The aggregation of Randomizers to use.</param>
        public Highest(Aggregation<int> inputs) : base(inputs)
        {
        }

        public override int Process(int[] inputs)
        {
            int result = Int32.MinValue;
            foreach(int x in inputs)
            {
                if(x > result)
                    result = x;
            }
            return result;
        }
    }
}
