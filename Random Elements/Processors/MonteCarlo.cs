using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Random_Elements.Generators;

namespace Random_Elements.Processors
{
    /// <summary>
    /// Performs a Monte Carlo simulation of a Generator.
    /// </summary>
    /// <typeparam name="T">The type of items to be generated.</typeparam>
    public class MonteCarlo<T> where T:notnull
    {
        /// <summary>
        /// The Generator to be used for running the simulation.
        /// </summary>
        public Generator<T> Generator { get; private set; }

        /// <summary>
        /// The results of the most recent simulation.
        /// </summary>
        public Dictionary<T,int> Results { get; private set; }

        /// <summary>
        /// A method for resetting the Generator.
        /// </summary>
        public createGenerator<T> Reset { get; private set; }

        /// <summary>
        /// Creates a new Monte Carlo simulator.
        /// </summary>
        /// <param name="reset">A function that creates the Generator to test.</param>
        public MonteCarlo(createGenerator<T> reset)
        {
            Generator = reset();
            Results = new Dictionary<T,int>();
            Reset = reset;
        }

        /// <summary>
        /// Runs a single trial.
        /// </summary>
        public void Trial()
        {
            T value = Generator.Peek();
            lock (Results)
            {
                if (Results.ContainsKey(value)) Results[value]++;
                else Results[value] = 1;
            }
        }

        /// <summary>
        /// Runs multiple trials
        /// </summary>
        /// <param name="trialCount">The number of trials to run.</param>
        public void RunTrials(int trialCount)
        {
            for(int i=0;i<trialCount;i++)
            {
                Trial();
                Generator=Reset();
            }
        }

        public void RunInParallel(int trialCount)
        {
            Parallel.For(0, trialCount, (int index) =>
            {
                Generator<T> generator = Reset();
                T value = generator.Peek();
                lock (Results)
                {
                    if (Results.ContainsKey(value)) Results[value]++;
                    else Results[value] = 1;
                }
            });
        }
    }

    /// <summary>
    /// A function that creates a Generator of the specified type.
    /// </summary>
    /// <typeparam name="T">The output type of the generator to create.</typeparam>
    /// <returns>A Generator that produces output of the specified type.</returns>
    public delegate Generator<T> createGenerator<T>();
}
