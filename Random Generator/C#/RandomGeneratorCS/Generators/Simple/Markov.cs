using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RandomGeneratorCS.Randomizer;
using Rand = RandomGeneratorCS.Randomizer.Randomizer;

namespace RandomGeneratorCS.Generators.Simple
{

    /// <summary>
    /// Uses a Markov chain to generate sequences that follow simple frequency rules.
    /// </summary>
    /// <typeparam name="T">The type of items generated.</typeparam>
    public class Markov<T> : Generator<T>, Poppable<T> where T : notnull
    {
        Dictionary<T, Dictionary<T,int>>? freqTable = null;
        T currentState;
        int solipsist;
        Rand rng;

        /// <summary>
        /// A new Markov chain.
        /// </summary>
        /// <param name="name">A unique identifier for the Markov.</param>
        /// <param name="initialState">The first element, already generated.</param>
        /// <param name="solipsistic">The amount by which an element becomes more likely each time it is selected.</param>
        /// <param name="randomizer">The random number generator used to produce the chain.</param>
        /// <param name="frequencyTable">The table used to generate the chain. Follows the pattern prev, next, frequency.</param>
        /// <param name="trainingData">Strings to be used for training the chain.</param>
        public Markov(string name, T initialState, int solipsistic = 0, Rand? randomizer = null, Dictionary<T,Dictionary<T,int>>? frequencyTable=null, T[][]? trainingData = null)
        {
            Name=name;
            Type="Markov";
            currentState=initialState;
            solipsist = solipsistic;
            if (randomizer != null)
                rng = randomizer;
            else
                rng = new SystemRandom(name + "RNG");
            Train(frequencyTable, trainingData);

        }
        public override T Peek()
        {
            return getItem((value) => { });
        }

        public T Pop(int bias=0)
        {
            return getItem((value) =>
            {
                TrainSingle(currentState, value, solipsist);
                currentState = value;
            });
        }

        public void Train(Dictionary<T,Dictionary<T,int>>? initial, T[][]? trainingData)
        {
            //Deep-clone the initial dictionary into the frequency table.
            if(freqTable == null)
                freqTable = new Dictionary<T,Dictionary<T,int>>();
            if(initial!=null)
                foreach(var pair in initial)
                {
                    foreach(var pair2 in pair.Value)
                        TrainSingle(pair.Key,pair2.Key,pair2.Value);

                }
            //Loop through each list of training data, adding it to the frequency table.
            if(trainingData!=null)
                foreach(T[] str in trainingData)
                {
                    for (int i = 0; i < freqTable.Count - 1; i++)
                    {
                        TrainSingle(str[i], str[i + 1], 1);
                    }
                    TrainSingle(str[str.Length - 1], str[0], 1);
                }
                
        }
        
        public void TrainSingle(T curr, T next, int bias=1)
        {
            if(!freqTable.ContainsKey(curr))
                freqTable.Add(curr, new Dictionary<T, int>());
            if (!freqTable[curr].ContainsKey(next))
                freqTable[curr].Add(next, bias);
            else
                freqTable[curr][next] += bias;
        }

        protected T getItem(ProcessLogic<T> process)
        {
            int total = 0;
            foreach (var item in freqTable[currentState].Values)
                total += item;
            int index=rng.Next(0, total);
            total = 0;
            foreach(var item in freqTable[currentState])
            {
                total += item.Value;
                if (total > index)
                {
                    process(item.Key);
                    return item.Key;
                }
            }
            throw new IndexOutOfRangeException();
        }

        public override Dictionary<string, object> Serialize()
        {
            var value= base.Serialize();
            value.Add("frequencyTable", freqTable);
            value.Add("currentState",currentState);
            value.Add("solipsism",solipsist);
            return value;
        }

        public override int Length => freqTable[currentState].Count;
    }
}
