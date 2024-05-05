using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Random_Elements.Processors.Aggregations
{
    /// <summary>
    /// Converts multiple Aggregations into a single Aggregation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Flatten<T>:Aggregation<T>
    {
        /// <summary>
        /// The underlying Aggregations to be used in creating the single Aggregation.
        /// </summary>
        public Aggregation<T>[] Inputs { get; private set; }

        public Flatten(Aggregation<T>[] inputs):base() { Inputs = inputs; }

        public override T[] peekLogic()
        {
            List<T> result=new List<T>();
            foreach(Aggregation<T> input in Inputs)
            {
                T[] intermediate= input.Peek();
                foreach(T t in intermediate)
                    result.Add(t);
            }
            return result.ToArray();
        }
    }
}
