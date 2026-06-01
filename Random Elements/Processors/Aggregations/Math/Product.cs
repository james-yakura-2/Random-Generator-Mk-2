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
    public class Product : Summary<int,int>
    {
        /// <summary>
        /// Creates a new Product.
        /// </summary>
        /// <param name="generators">The generator set </param>
        public Product(Aggregation<int> generators):base(generators)
        {
        }

        public override int Process(int[] inputs)
        {
            int result = 1;
            foreach (int x in inputs)
                result *= x;
            return result;
        }
    }
}