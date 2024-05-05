using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Random_Elements.Randomizers;

namespace Random_Elements.Generators
{
    /// <summary>
    /// A Urn that has a random chance of executing its default behavior even if it still contains items.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EndlessUrn<T>:Urn<T>
    {
        /// <summary>
        /// Recalculate a random chance.
        /// </summary>
        /// <returns>The fraction in the form of an array {Numerator, Denominator}.</returns>
        public delegate int[] RecalculateFraction();

        /// <summary>
        /// The numerator of the chance of default behavior.
        /// </summary>
        public int DefaultNumerator { get; set; }

        /// <summary>
        /// The denominator of the chance of default behavior.
        /// </summary>
        public int DefaultDenominator { get; set; }

        /// <summary>
        /// The function to be used for recalculating the chance of default behavior.
        /// </summary>
        /// <example>
        /// OneRandomToken()
        /// {
        ///     
        /// }
        /// </example>
        public RecalculateFraction Recalculate { get; set; }

        public EndlessUrn(T[] initial, Randomizer rng, Default baseline, Restore revert, RecalculateFraction recalculate):base(initial,rng,baseline,revert)
        {
            Recalculate = recalculate;
        }

        public override T peekLogic()
        {
            T value;
            if (RNG.Next(DefaultDenominator) <= DefaultNumerator)
            {
                value = WhenEmpty();
            }
            else
                value = base.peekLogic();
            return value;
        }

        public override T popLogic()
        {
            T value;
            if (RNG.Next(DefaultDenominator) <= DefaultNumerator)
            {
                value = WhenEmpty();
            }
            else
            {
                value = base.popLogic();
            }
            UpdateFraction();
            return value;
        }

        public override void pushLogic(T value)
        {
            base.pushLogic(value);
            UpdateFraction();
        }

        public override T WhenEmpty()
        {
            T value= base.WhenEmpty();
            UpdateFraction();

            return value;
        }

        /// <summary>
        /// Recalculates the chance of random default behavior.
        /// </summary>
        private void UpdateFraction()
        {
            int[] newFraction = Recalculate();
            DefaultNumerator = newFraction[0];
            DefaultDenominator = newFraction[1];
        }
    }
}
