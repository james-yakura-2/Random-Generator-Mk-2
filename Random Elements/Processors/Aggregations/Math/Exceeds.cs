using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Random_Elements.Processors.Aggregations.Math
{
    /// <summary>
    /// Removes all results that do not exceed a target number.
    /// </summary>
    public class Exceeds:ResultWiseFilter<int>
    {
        /// <summary>
        /// The target number to compare results to.
        /// </summary>
        int Target { get; set; }

        public Exceeds(Aggregation<int> inputs, int target):base(inputs)
        {
            Target = target;
        }

        public override bool Include(int obj)
        {
            return obj > Target;
        }
    }
}
