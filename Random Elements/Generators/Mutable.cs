using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Random_Elements.Generators
{
    /// <summary>
    /// Indicates that a Generator can have elements added and removed.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface Mutable<T>
    {
        /// <summary>
        /// Retrieve an item from the Mutable while changing its state.
        /// </summary>
        /// <returns>A randomly selected item.</returns>
        public T Pop()
        {
            T result = popLogic();
            if(this is Generator<T>)
            {
                ((Generator<T>)this).Previous=result;
            }
            return result;
        }

        /// <summary>
        /// Logic to retrieve an item from the Mutable while changing its state.
        /// </summary>
        /// <remarks>popLogic should never be directly called, since it is not logged.</remarks>
        /// <returns>A randomly selected item.</returns>
        public T popLogic();

        /// <summary>
        /// Adds an item to the Mutable.
        /// </summary>
        /// <param name="value">The item to be added.</param>
        public void Push(T value)
        {
            pushLogic(value);
        }

        /// <summary>
        /// Logic to add an item to the Generator.
        /// </summary>
        /// <remarks>pushLogic should not be called directly, since universal logic may be stored in pushLogic.</remarks>
        /// <param name="value">The item to be added.</param>
        public void pushLogic(T value);

        /// <summary>
        /// The behavior to use when attempting to <see cref="Generator{T}.Peek()"/> or <see cref="Mutable{T}.Pop()"/> an empty Generator.
        /// Behaviors might include returning a dummy value, generating a new item, or throwing an exception.
        /// </summary>
        public T WhenEmpty();

    }
}
