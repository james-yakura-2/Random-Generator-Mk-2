using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Random_Elements.Generators;

namespace Random_Elements.Processors.Aggregations
{
    /// <summary>
    /// A generator that explodes if the result is greater than or equal to a target number.
    /// </summary>
    public class ExplodeIfGE:Exploding<int>
    {
        public int TargetNumber { get; private set; }

        public ExplodeIfGE(Generator<int> generator, int target):base(generator)
        {
            TargetNumber = target;
        }

        public override bool Explode(int item)
        {
            return item>=TargetNumber;
        }
    }
}
