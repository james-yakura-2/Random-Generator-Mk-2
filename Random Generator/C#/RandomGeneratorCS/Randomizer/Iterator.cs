using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomGeneratorCS.Randomizer
{
    /// <summary>
    /// A Randomizer that iterates through items when randomizing.
    /// </summary>
    public class Iterator:Randomizer
    {
        int current = 0;

        /// <summary>
        /// Creates a new Iterator.
        /// </summary>
        /// <param name="name">A unique name for the Iterator.</param>
        /// <param name="start">The initial value for the Iterator.</param>
        public Iterator(string name,int start=0)
        {
            Name= name;
            current = start;
        }

        public override int Next(int min, int max)
        {
            current++;
            return min+((current-1)%(max-min));
        }

        public override Dictionary<string, object> Serialize()
        {
            Dictionary<string,object> value= base.Serialize();
            value.Add("current", current);
            return value;
        }
    }
}
