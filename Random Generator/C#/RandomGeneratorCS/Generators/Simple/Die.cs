using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RandomGeneratorCS.Randomizer;
using Rand=RandomGeneratorCS.Randomizer.Randomizer;

namespace RandomGeneratorCS.Generators.Simple
{
    /// <summary>
    /// A basic stateless generator, that produces random elements based entirely on its contents.
    /// </summary>
    /// <typeparam name="T">The type of items produced.</typeparam>
    public class Die<T> : Generator<T> where T:notnull
    {
        protected readonly Dictionary<T,int> cont=new();

        readonly T? defaultVal = default;
        readonly Generator<T>? defaultGen=null;

        /// <summary>
        /// Creates a new Die.
        /// </summary>
        /// <param name="name">A unique name for the Die.</param>
        /// <param name="contents">The starting contents of the Die.</param>
        /// <param name="defaultValue">The value to return if the die roll somehow fails.</param>
        /// <param name="defaultGenerator">Another Generator to use if the die somehow fails.</param>
        /// <param name="generator">The random number generator to use with this Die.</param>
        public Die(string name, Dictionary<T,int>? contents=null, T? defaultValue=default, Generator<T>? defaultGenerator=null, Rand? generator=null )
        {
            Name = name;
            Type = "Die";
            if (contents != null)
            {
                foreach (KeyValuePair<T,int> item in contents)
                {
                    cont[item.Key] = item.Value;
                }
            }
            if (generator == null)
            {
                RNG = new SystemRandom(name + "RNG");
            }
            else
            {
                RNG = generator;
            }
            defaultVal = defaultValue;
            defaultGen = defaultGenerator;
        }

        public override T Peek()
        {
            return getElement((result) => { }, (Generator<T> gen) => { return gen.Peek(); });
        }
 
        public override Dictionary<string, object> Serialize()
        {
            var value=base.Serialize();
            value.Add("contents",cont.ToArray());
            if (defaultGen != null)
                value.Add("defaultGenerator", defaultGen);
            if(defaultVal != null)
                value.Add("defaultValue",defaultVal);
            return value;
        }

        protected T getElement(ProcessLogic<T> processLogic, GeneratorLogic<T> generatorLogic)
        {
            int total = 0;
            foreach(KeyValuePair<T,int> item in cont)
            {
                total += item.Value;
            }
            int index = RNG.Next(0,total);
            total = 0;
            foreach(KeyValuePair<T,int> item in cont)
            {
                total+=item.Value;
                if (total > index)
                    processLogic(item.Key);
                    return item.Key;
            }
            if (defaultGen != null)
                return generatorLogic(defaultGen);
            return defaultVal;

        }

        public override int Length => cont.Count;
    }

    public delegate void ProcessLogic<T>(T result);

    public delegate T GeneratorLogic<T>(Generator<T> generator);
}
