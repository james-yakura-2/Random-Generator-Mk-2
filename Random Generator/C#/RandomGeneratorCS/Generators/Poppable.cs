using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomGeneratorCS.Generators
{
    public interface Poppable<T>
    {
        public abstract T? Pop(int bias=1);
    }
}
