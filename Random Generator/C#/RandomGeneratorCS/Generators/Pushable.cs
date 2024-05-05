using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomGeneratorCS.Generators
{
    public interface Pushable<T>
    {
        public abstract void Push(T item, int bias=1);
    }
}
