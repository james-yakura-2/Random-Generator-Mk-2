using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Random_Elements.Processors.Aggregations.Math
{
    /// <summary>
    /// Removes the generator with the lowest falue from a set.
    /// </summary>
    public class DropLowest : Filter<int>
    {
        public DropLowest(Aggregation<int> inputs) : base(inputs)
        { }

        public override int[] RunFilter(int[] inputs)
        {
            int lowest = int.MaxValue;
            int lowestIndex = -1;
            for(int i = 0; i < inputs.Length; i++)
                if(inputs[i] <= lowest)
                {
                    lowestIndex = i;
                    lowest = inputs[i];
                }
            List<int> result = new List<int>();
            for(int i = 0; i < inputs.Length; i++)
                if(i!=lowestIndex)
                    result.Add(inputs[i]);
            return result.ToArray();
        }
    }
}
