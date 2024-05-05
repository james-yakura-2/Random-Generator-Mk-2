using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Random_Elements.Generators;

namespace Random_Elements.Processors.Aggregations
{
    /// <summary>
    /// Builds an Aggregration from a single Generator, that can conditionally add elements.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Exploding<T>:Aggregation<T>
    {

        public Generator<T> Generator { get; private set; }

        public Exploding(Generator<T> generator)
        {
            Generator = generator;
        }

        public abstract bool Explode(T item);

        public override T[] peekLogic()
        {
            List<T> result=new List<T>();
            T newItem;
            do
            {
                newItem=Generator.Peek();
                result.Add(newItem);
            }while(Explode(newItem));
            return result.ToArray();
        }

    }
}
