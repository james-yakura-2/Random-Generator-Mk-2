using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Random_Elements.Randomizers;

namespace Random_Elements.Generators
{
    /// <summary>
    /// Represents a die. Generates outcomes with equal weighting and no path-dependence.
    /// </summary>
    /// <typeparam name="T">The type of items generated.</typeparam>
    public class Die<T>:Generator<T>
    {
        /// <summary>
        /// The faces of the Die.
        /// </summary>
        public T[] Contents { get; set; }
        public Randomizer RNG { get; protected set; }

        public Die(T[] contents, Randomizer rng)
        {
            Contents = contents;
            RNG = rng;
        }

        public override T peekLogic()
        {
            return Contents[RNG.Next(Contents.Length)];
        }

        public static Die<int> NumberedDieFactory(int size, Randomizer rng)
        {
            int[] faces=new int[size];
            for (int i = 0; i < size; i++)
                faces[i] = i + 1;
            return new Die<int>(faces, rng);
        }

        public static Die<T> DieFactory(Randomizer rng)
        {
            return new Die<T>(new T[0],rng);
        }
    }
}
