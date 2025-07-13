using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Random_Elements.Generators;

namespace Random_Elements.Processors
{
    /// <summary>
    /// Converts one type of result to another.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    public abstract class Conversion<T,U>:Generator<T>
    {
        public Generator<U> Input { get; set; }

        /// <summary>
        /// Convert an input of type U to an output of type T.
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public abstract T Process(U u);

        /// <summary>
        /// Convert an output of type T to an input of type U.
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public abstract U Downprocess(T t);

        public override T peekLogic()
        {
            return Process(Input.Peek());
        }
    }
}
