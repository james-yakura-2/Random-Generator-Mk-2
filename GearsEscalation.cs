using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Random_Elements.Processors;
using Random_Elements.Processors.Aggregations;

namespace Random_Generator_Mk_2
{
    /// <summary>
    /// Any set of matching dice are replaced with a die higher by the length of the set, minus one; a player may choose to strategically treat a set as smaller than it actually is. Then, return the highest die.
    /// </summary>
    internal class GearsEscalation:Summary<int,int>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        public GearsEscalation(Aggregation<int> input) : base(input)
        {
        }

        public override int Process(int[] inputs)
        {
            bool running = false;
            int[] _input = inputs;
            do
            {
                running = false;
                int highest = 0;
                foreach (int die in _input)
                {
                    if (die > highest)
                    {
                        highest = die;
                    }
                }
                List<int> processed = new List<int>();
                for (int i = 0; i <= highest; i++)
                {
                    int count = 0;
                    foreach (int die in _input)
                    {
                        if (die == i)
                        {
                            count++;
                        }
                    }
                    if (count >= 1)
                        for(int j=0;j<count; j++)
                            processed.Add(i+j);
                    if (count > 1)
                        running = true;
                }
                _input = processed.ToArray();
            } while (running);
            int result = 0;
            foreach (int die in _input)
            {
                if(die > result)
                    result = die;
            }
            return result;
        }
    }
}
