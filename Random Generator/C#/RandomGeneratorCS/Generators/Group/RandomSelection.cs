using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomGeneratorCS.Generators.Group
{
    /// <summary>
    /// Selects an item from the generated items at random.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RandomSelection<T>:MultiGenerator<T>
    {
        public override T ProcessResults(T[] results)
        {
            int index=RNG.Next(0,results.Length);
            return results[index];
        }
    }
}
