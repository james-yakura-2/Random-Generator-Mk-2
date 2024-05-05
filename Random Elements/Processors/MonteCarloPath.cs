using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Random_Elements.Generators;

namespace Random_Elements.Processors
{
    /// <summary>
    /// A single run of a Monte Carlo simulation.
    /// </summary>
    /// <typeparam name="T">The type of data to generate.</typeparam>
    public abstract class MonteCarloPath<T>:Generator<T>
    {
        /// <summary>
        /// Whether the path is completed.
        /// </summary>
        public bool Done { get; set; } = false;


        public override T peekLogic()
        {
            while(!Done)
            {
                iterate();
            }
            return output();
        }

        /// <summary>
        /// A single step of the simulation.
        /// </summary>
        public abstract void iterate();
        /// <summary>
        /// Evaluate the final outcome of the simulation.
        /// </summary>
        /// <returns>The result of the simulation.</returns>
        public abstract T output();
    }
}
