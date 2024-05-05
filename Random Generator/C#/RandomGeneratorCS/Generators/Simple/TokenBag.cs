using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomGeneratorCS.Generators.Simple
{
    /// <summary>
    /// A bag of tokens from which items can be drawn with replacement (Peek) or without (Pop).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TokenBag<T>:Die<T>, Poppable<T> where T:notnull
    {
        public TokenBag(string name, T? defaultValue=default, Generator<T>? defaultGenerator=null, Dictionary<T,int>? contents=null, Randomizer.Randomizer? randomizer=null):base(name,defaultValue:defaultValue,defaultGenerator:defaultGenerator,contents:contents,generator:randomizer)
        {
            Type = "TokenBag";
        }

        public T Pop(int bias = 1)
        {
            return getElement((item) => { cont[item] -= bias; if (cont[item] < 0) cont[item] = 0; }, (Generator<T> gen) => 
            {
                if (gen is Poppable<T>)
                    return ((Poppable<T>)gen).Pop();
                else
                    return gen.Peek();
            });
        }

        public void Push(T item, int bias = 1)
        {
            if (!cont.ContainsKey(item))
                cont[item] = bias;
            else
                cont[item] += bias;
        }
    }
}
