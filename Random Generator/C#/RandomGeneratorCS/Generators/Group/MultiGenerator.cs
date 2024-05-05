using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RandomGeneratorCS.Generators.Simple;
using RandomGeneratorCS.Generators;

namespace RandomGeneratorCS.Generators.Group
{
    /// <summary>
    /// Produces a summary of the results of multiple generators.
    /// </summary>
    /// <typeparam name="T">The return type of the underlying generators.</typeparam>
    /// <typeparam name="U">The return type of the summary.</typeparam>
    public abstract class MultiGenerator<T> : Generator<T>, Poppable<T>
        
    {
        Generator<T>[] elements;

        public Generator<T>[] Elements { get { return elements; } set { elements = value; } }

        public override T Peek()
        {
            T[] results = new T[elements.Length];
            for (int i = 0; i < elements.Length; i++)
            {
                results[i] = elements[i].Peek();
            }
            return ProcessResults(results);
        }

        public T Pop(int bias = 1)
        {
            T[] results = new T[elements.Length];
            for (int i = 0; i < elements.Length; i++)
            {
                if (elements[i] is Poppable<T>)
                    results[i] = ((Poppable<T>)elements[i]).Pop(bias);
                else
                    results[i] = elements[i].Peek();
            }
            return ProcessResults(results);
        }

        public abstract T ProcessResults(T[] results);

        public override int Length
        {
            get {
                int max = 0;
                for (int i = 0; i < elements.Length; i++)
                {
                    if (elements[i].Length > max)
                        max = elements[i].Length;
                }
                return max;
            }
        }
    }
}
