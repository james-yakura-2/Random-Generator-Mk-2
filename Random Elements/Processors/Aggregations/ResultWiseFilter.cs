using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Random_Elements.Processors.Aggregations
{
    /// <summary>
    /// Filters a set of results by data particular to each result, such as exceeding a target number.
    /// </summary>
    /// <typeparam name="T">The type of the results to be passed.</typeparam>
    public abstract class ResultWiseFilter<T>:Filter<T>
    {
        public ResultWiseFilter(Aggregation<T> inputs):base(inputs)
        {

        }

        public abstract bool Include(T obj);

        public override T[] RunFilter(T[] inputs)
        {
            List<T> result = new List<T>(); 
            foreach (T input in inputs)
                if(Include(input))
                    result.Add(input);
            return result.ToArray();
        }
    }
}
