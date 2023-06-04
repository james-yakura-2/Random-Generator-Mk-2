using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Random_Generator_Mk_2.Generators;

namespace Random_Generator_Mk_2.Processors
{
    public abstract class Conversion<T,U>:Generator<T>, Mutable<T>
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

        public override T peek()
        {
            return Process(Input.peek());
        }

        T Mutable<T>.pop()
        {
            if (Input is Mutable<U>)
                return Process(((Mutable<U>)Input).pop());
            else
                return Process(Input.peek());
        }

        void Mutable<T>.push(T value)
        {
            if (Input is Mutable<U>)
                ((Mutable<U>)Input).push(Downprocess(value));
        }
    }
}
