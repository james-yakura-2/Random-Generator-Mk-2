using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Random_Generator_Mk_2.Generators;

namespace Random_Generator_Mk_2.Processors
{
    /// <summary>
    /// Runs a set of generators in bulk.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GeneratorSet<T> : Generator<T[]>, Mutable<T[]>
    {
        public Generator<T>[] Inputs { get; private set; }

        public GeneratorSet(Generator<T>[] contents)
        {
            Inputs = contents;
        }

        public GeneratorSet(GeneratorSet<T> inputs):this(inputs.Inputs)
        {

        }

        public override T[] peek()
        {
            T[] result=new T[Inputs.Length];
            for (int i=0; i<result.Length; i++)
                result[i] = Inputs[i].peek();
            return result;
        }

        public virtual T[] pop()
        {
            T[] result = new T[Inputs.Length];
            for(int i=0; i<result.Length; i++)
            {
                if (Inputs[i] is Mutable<T>)
                    result[i] = ((Mutable<T>)Inputs[i]).pop();
                else
                    result[i] = Inputs[i].peek();
            }
            return result;
        }

        public virtual void push(T[] value)
        {
            for(int i=0; i<value.Length; i++)
                if(Inputs[i] is Mutable<T>)
                    ((Mutable<T>)Inputs[i]).push(value[i]);
        }
    }
}
