using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomGeneratorCS.Generators
{
    public interface Shufflable<T>:Pushable<T>
    {
        public abstract void Shuffle();

        public abstract void PushTo(T item, int index);
    }
}
