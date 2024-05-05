using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Random_Elements.Randomizers;

namespace Random_Elements.Generators
{
    /// <summary>
    /// A generator that can eliminate elements when they are drawn.
    /// </summary>
    /// <typeparam name="T">The type of elements to generate.</typeparam>
    public class Urn<T>:Generator<T>, Mutable<T>
    {
        /// <summary>
        /// The default action for the Urn, taken if it uses <see cref="Generator{T}.Peek()"/> or <see cref="Mutable{T}.Pop()"/> while empty.
        /// </summary>
        /// <returns>An element of the Urn's type.</returns>
        /// <exception cref="IndexOutOfRangeException">The Urn is empty and has no default behavior.</exception>
        public delegate T Default();

        /// <summary>
        /// A function to remove data from the Urn.
        /// </summary>
        /// <param name="level">Roughly corresponds to the level of information retained.</param>
        /// <returns>The Urn's contents at the designated state.</returns>
        /// <example>
        /// List{T[]} Snapshots;
        /// 
        /// public void Snapshot()
        /// {
        ///     Snapshots.Add(Contents.ToArray());
        /// }
        /// 
        /// //The delegate.
        /// T[] Rollback(int level)
        /// {
        ///     return Snapshots[level];
        /// }
        /// </example>
        /// <example>
        /// List{T} allTokens;
        /// 
        /// public override void pushLogic(T value)
        /// {
        ///     base.Push(value);
        ///     allTokens.Add(value);
        /// }
        /// 
        /// //The delegate.
        /// T[] Retrieve(int level)
        /// {
        ///     List{T} value=new List{T}();
        ///     for(int i=level; i<value; i++)
        ///     {
        ///         value.Add(allTokens[i]);
        ///     }
        ///     return value;
        /// }
        /// </example>
        public delegate T[] Restore(int level);

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
        /// The behavior to use to reset the Urn.
        /// </summary>
        public Restore Revert { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="initial">The initial contents of the Urn.</param>
        /// <param name="rng">The Randomizer used to generate the Urn's behavior.</param>
        /// <param name="baseline">The default action for the Urn, taken if it uses <see cref="Generator{T}.Peek()"/> or <see cref="Mutable{T}.Pop()"/> while empty.</param>
        /// <param name="revert">The behavior used to reset the Urn. See <see cref="Restore" /> for examples.</param>
        public Urn(T[] initial, Randomizer rng, Default baseline, Restore revert):base()
        {
            Baseline = baseline;
            Contents = new List<T>(initial);
            RNG = rng;
            Revert = revert;
        }

        public override T peekLogic()
        {
            T result;
            if(Contents.Count>0)
            {
                int index=RNG.Next(Contents.Count);
                result=Contents[index];
            }
            else
            {
                ((Mutable<T>)this).Push(((Mutable<T>)this).WhenEmpty());
                result=Peek();
            }
            return result;
        }

        public virtual T popLogic()
        {
            T result;
            if (Contents.Count > 0)
            {
                int index = RNG.Next(Contents.Count);
                result = Contents[index];
                Contents.RemoveAt(index);
            }
            else
            {
                ((Mutable<T>)this).Push(((Mutable<T>)this).WhenEmpty());
                result = ((Mutable<T>)this).Pop();
            }
            return result;
        }

        public virtual void pushLogic(T value)
        {
            Contents.Add(value);
        }

        public void Shuffle(int retention = 0)
        {
            Revert(retention);
        }

        public virtual T WhenEmpty()
        {
            return Baseline();
        }
    }
}
