using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Random_Elements.Randomizers;

namespace Random_Elements.Generators
{
    /// <summary>
    /// A Generator that adds items drawn from it.
    /// </summary>
    /// <typeparam name="T">The type of items to generate.</typeparam>
    public class PolyaUrn<T> : Generator<T>, Mutable<T>
    {
        /// <summary>
        /// The default action for the Urn, taken if it uses <see cref="Generator{T}.Peek()"/> or <see cref="Mutable{T}.Pop()"/> while empty.
        /// </summary>
        /// <returns>An element of the Urn's type.</returns>
        /// <exception cref="IndexOutOfRangeException">The Urn is empty and has no default behavior.</exception>
        public delegate T Default();


        /// <summary>
        /// The default action for the Urn, taken if it uses <see cref="Generator{T}.Peek()"/> or <see cref="Mutable{T}.Pop()"/> while empty.
        /// </summary>
        public Default Baseline { get; protected set; }

        /// <summary>
        /// The current contents of the Urn.
        /// </summary>
        public List<T> Contents { get; protected set; }

        /// <summary>
        /// The Randomizer that the Urn uses.
        /// </summary>
        public Randomizer RNG { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="initial">The initial contents of the Urn.</param>
        /// <param name="rng">The Randomizer used to generate the Urn's behavior.</param>
        /// <param name="baseline">The default action for the Urn, taken if it uses <see cref="Generator{T}.Peek()"/> or <see cref="Mutable{T}.Pop()"/> while empty.</param>
        /// <param name="revert">The behavior used to reset the Urn. See <see cref="Restore" /> for examples.</param>
        public PolyaUrn(T[] initial, Randomizer rng, Default baseline) : base()
        {
            Baseline = baseline;
            Contents = new List<T>(initial);
            RNG = rng;
        }

        public override T peekLogic()
        {
            T result;
            if (Contents.Count > 0)
            {
                int index = RNG.Next(Contents.Count);
                result = Contents[index];
            }
            else
            {
                ((Mutable<T>)this).Push(((Mutable<T>)this).WhenEmpty());
                result = Peek();
            }
            return result;
        }

        public virtual T popLogic()
        {
            T result = this.Peek();
            ((Mutable<T>)this).Push(result);
            return result;
        }

        public virtual void pushLogic(T value)
        {
            Contents.Add(value);
        }

        public virtual T WhenEmpty()
        {
            return Baseline();
        }
    }
}
