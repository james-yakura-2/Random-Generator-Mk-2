using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Random_Elements.Generators;

namespace Random_Elements.Processors.Aggregations
{
    /// <summary>
    /// Produces multiple results from a single pull.
    /// </summary>
    /// <typeparam name="T">The type of results to generate.</typeparam>
    public abstract class Aggregation<T> : Generator<T[]>
    {


    } 
}
