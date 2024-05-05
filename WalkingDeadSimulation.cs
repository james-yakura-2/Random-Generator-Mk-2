using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Random_Elements.Generators;
using Random_Elements.Processors;
using Random_Elements.Processors.Aggregations;
using Random_Elements.Processors.Aggregations.Math;
using Random_Elements.Randomizers;

namespace Random_Generator_Mk_2
{
    internal class WalkingDeadSimulation:MonteCarloPath<int>
    {
        Aggregation<int> baseDicePool;
        Aggregation<int> stressDicePool;
        Summary<int, int> rollOutput;
        Summary<int, int> botches;
        int rolls = 0;
        int threat = 0;
        Randomizer _rng;

        public WalkingDeadSimulation(int dice, Randomizer rng)
        {
            _rng = rng;
            Die<int>[] _dice = new Die<int>[dice];
            for (int i = 0; i < _dice.Length; i++)
                _dice[i] = new Die<int>(new int[]{ 1, 2, 3, 4, 5, 6 },rng);
            baseDicePool = new MultiGenerator<int>(_dice);
            stressDicePool = new MultiGenerator<int>(new Die<int>[0]);
            Flatten<int> allDice=new Flatten<int>(new Aggregation<int>[]{baseDicePool,stressDicePool });
            Aggregation<int> successes = new MatchingFilter<int>(baseDicePool, new int[]{ 6});
            rollOutput=new Count<int>(successes);
            Aggregation<int> critfailures=new MatchingFilter<int>(stressDicePool, new int[]{ 1});
            botches=new Count<int>(critfailures);
        }

        public override void iterate()
        {
            rolls++;
            //Check for successes. If a failure, increase Stress.
            int success = rollOutput.Peek() ;
            if (success==0)
            {
                //DEBUG
                /*foreach (Generator<int> x in ((MultiGenerator<int>)baseDicePool).Inputs)
                    Console.Write(x.Previous + ",");
                Console.Write("|");
                foreach (Generator<int> x in ((MultiGenerator<int>)stressDicePool).Inputs)
                    Console.Write(x.Previous + ",");
                Console.WriteLine("");
                */
                //END DEBUG
                Die<int> newDie = new Die<int>(new int[] { 1, 2, 3, 4, 5, 6 }, _rng);
                ((MultiGenerator<int>)stressDicePool).Inputs.Add(newDie);
                botches.Peek();
                success= rollOutput.Peek();
            }
            //DEBUG
            /*foreach (Generator<int> x in ((MultiGenerator<int>)baseDicePool).Inputs)
                Console.Write(x.Previous + ",");
            Console.Write("|");
            foreach (Generator<int> x in ((MultiGenerator<int>)stressDicePool).Inputs)
                Console.Write(x.Previous + ",");
            Console.WriteLine("");
            */
            //END DEBUG
            //Check Stress. If a botch, increase Threat.
            int critfails = botches.Previous;
            if (critfails > 0) 
                threat++;
            //Console.WriteLine("Successes:" + success + " Botches:" + critfails + " Threat Level:" + threat);
            //Console.ReadLine();
            //If Threat is 3, end the simulation.
            if (threat >= 3)
                Done = true;
        }

        public override int output()
        {
            return rolls;
        }
    }
}
