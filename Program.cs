// See https://aka.ms/new-console-template for more information

using Random_Elements.Generators;
using Random_Elements.Randomizers;
using Random_Elements.Processors;
using Random_Elements.Processors.Aggregations;
using Random_Elements.Processors.Aggregations.Math;
using Random_Generator_Mk_2;

string[] YES = { "Y", "y", "yes", "Yes", "YES" };
string[] NO = { "N", "n", "no", "No", "NO" };
string[] ATTRIBUTES = { "STR", "DEX", "CON", "INT", "WIS", "CHA" };
int MONTE_CARLO_TRIALS = 2900000;
Randomizer rng = new SystemRandom();
string SerializeArray<T>(T[] array)
{
    string result = "[";
    for(int i = 0; i < array.Length; i++)
    {
        result+=array[i].ToString();
        if(i<array.Length-1)
        {
            result += ",";
        }
    }
    result+="] ("+array.Length+" items)";
    return result;
}
string SerializeFrequencyTable<T>(Dictionary<T, int> dictionary)
{
    string result = "{\n";
    List<T> keys=dictionary.Keys.ToList();
    if(keys[0] is IComparable)
        keys.Sort();
    float total = 0;
    foreach(T key in keys)
    {
        float probability=0.0F;
        result += "\t" + key.ToString() + "    \t:\t";
        if (dictionary[key] != null)
        {
            probability = (float)(dictionary[key]) / MONTE_CARLO_TRIALS;
            result += dictionary[key] + "    \t:\t" + probability;
        }
        else
            result += "<NULL>";
        total += probability;
        result += "    \t:\t" + total;
        result += "\n";
    }
    result += "}";
    return result;
}


// ***Begin setup logic***

// D&D Character Creation
//int sides = 6;
//int[] results=new int[sides];
//for (int i = 0; i < sides; i++)
//{
//    results[i] = i + 1;
//}
//Die<int> die=new Die<int>(results,rng);
//Aggregation<int> fourDice=new Aggregation<int>(new Die<int>[] {die,die,die,die});
//Aggregation<int> threeDice=new DropLowest(fourDice);
//Sum sum=new Sum(threeDice);

//World of Darkness die rolls
/*
int sides = 10;
int[] results = new int[sides];
for (int i = 0; i < sides; i++)
{
    results[i] = i + 1;
}
Die<int> die = new Die<int>(results, rng);
Exploding<int> explodingDie=new ExplodeIfGE(die,10);
*/


// ***End setup logic***

bool running =true;
while (running)
{

    // ***Begin main logic***

    //D&D Character Creation rolls
    //foreach(string x in ATTRIBUTES)
    //{
    //    Console.WriteLine(x + " " + sum.Peek());
    //}

    //World of Darkness die rolls
    //System.Console.WriteLine("How many dice?");
    //int dice=Convert.ToInt32(System.Console.ReadLine());
    //Aggregation<int>[] explodingDice=new Aggregation<int>[dice];
    //for (int i = 0; i < dice; i++)
    //    explodingDice[i] = explodingDie;
    //Flatten<int> flattenedPool=new Flatten<int>(explodingDice);
    //Count<int> dicePool = new Count<int>(new Exceeds(flattenedPool, 7));
    //int successes = dicePool.Peek();
    //System.Console.WriteLine(successes+" successes! "+SerializeArray<int>(flattenedPool.Previous));


    //World of Darkness Monte Carlo
    //System.Console.WriteLine("How many dice?");
    //int dice = Convert.ToInt32(System.Console.ReadLine());
    //Aggregation<int>[] explodingDice = new Aggregation<int>[dice];
    //for (int i = 0; i < dice; i++)
    //    explodingDice[i] = explodingDie;
    //Flatten<int> flattenedPool = new Flatten<int>(explodingDice);
    //Count<int> dicePool = new Count<int>(new Exceeds(flattenedPool, 7));

    //MonteCarlo<int> simulation = new MonteCarlo<int>(() => { return dicePool; });
    //simulation.RunInParallel(MONTE_CARLO_TRIALS);
    //Console.WriteLine(SerializeFrequencyTable<int>(simulation.Results));


    //Walking Dead rolls until confrontation
    //System.Console.WriteLine("How many dice?");
    //int dice = Convert.ToInt32(System.Console.ReadLine());
    //MonteCarlo<int> simulation = new MonteCarlo<int>(() => { return new WalkingDeadSimulation(dice, rng); });
    //simulation.RunInParallel(MONTE_CARLO_TRIALS);
    //Console.WriteLine(SerializeFrequencyTable<int>(simulation.Results));


    //River test
    //bool testing = true;
    //River<bool> simulation= new River<bool>(new bool[] { true, false }, rng, () => { throw new NotImplementedException(); });
    //while (testing)
    //{
    //    Console.WriteLine(((Mutable<bool>)simulation).Pop());
    //    Console.WriteLine(SerializeArray<bool>(simulation.Contents.ToArray()));
    //    bool gettingValue = true;
    //    while (gettingValue)
    //    {
    //        System.Console.WriteLine("Draw again?");
    //        string result = System.Console.ReadLine();
    //        if (YES.Contains<string>(result))
    //            gettingValue = false;
    //        if (NO.Contains<string>(result))
    //        {
    //            gettingValue = false;
    //            testing = false;
    //        }

    //    }

    //}

    //Science Adventure failures
    System.Console.WriteLine("How many dice?");
    int dice = Convert.ToInt32(System.Console.ReadLine());
    //Dictionary<int, int> mapping = new Dictionary<int, int>() 
    //{ {0, 0 },
    //{1, 3},
    //    {2,2},
    //    {3,2},
    //    {4,1 },
    //    {5,0 }
    //};
    MonteCarlo<int> d4simulation = new MonteCarlo<int>(() =>
    {
        Die<int>[] diceArray=new Die<int>[dice];
        for(int i = 0; i < diceArray.Length; i++)
        {
            diceArray[i] = new Die<int>(new int[]{ 1,2,3,4 }, rng);
        }
        return new Highest(new MultiGenerator<int>(diceArray));
    });
    d4simulation.RunInParallel(MONTE_CARLO_TRIALS);
    Console.WriteLine("d4:\n"+SerializeFrequencyTable<int>(d4simulation.Results));
    MonteCarlo<int> d6simulation = new MonteCarlo<int>(() =>
    {
        Die<int>[] diceArray = new Die<int>[dice];
        for (int i = 0; i < diceArray.Length; i++)
        {
            diceArray[i] = new Die<int>(new int[] { 1, 2, 3, 4, 5, 6 }, rng);
        }
        return new Highest(new MultiGenerator<int>(diceArray));
    });
    d6simulation.RunInParallel(MONTE_CARLO_TRIALS);
    Console.WriteLine("d6:\n" + SerializeFrequencyTable<int>(d6simulation.Results));
    MonteCarlo<int> d8simulation = new MonteCarlo<int>(() =>
    {
        Die<int>[] diceArray = new Die<int>[dice];
        for (int i = 0; i < diceArray.Length; i++)
        {
            diceArray[i] = new Die<int>(new int[] { 1, 2, 3, 4, 5, 6,7,8 }, rng);
        }
        return new Highest(new MultiGenerator<int>(diceArray));
    });
    d8simulation.RunInParallel(MONTE_CARLO_TRIALS);
    Console.WriteLine("d8:\n" + SerializeFrequencyTable<int>(d8simulation.Results));
    MonteCarlo<int> d10simulation = new MonteCarlo<int>(() =>
    {
        Die<int>[] diceArray = new Die<int>[dice];
        for (int i = 0; i < diceArray.Length; i++)
        {
            diceArray[i] = new Die<int>(new int[] { 1, 2, 3, 4, 5,6,7,8,9,10 }, rng);
        }
        return new Highest(new MultiGenerator<int>(diceArray));
    });
    d10simulation.RunInParallel(MONTE_CARLO_TRIALS);
    Console.WriteLine("d10:\n" + SerializeFrequencyTable<int>(d10simulation.Results));
    MonteCarlo<int> d12simulation = new MonteCarlo<int>(() =>
    {
        Die<int>[] diceArray = new Die<int>[dice];
        for (int i = 0; i < diceArray.Length; i++)
        {
            diceArray[i] = new Die<int>(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10,11,12 }, rng);
        }
        return new Highest(new MultiGenerator<int>(diceArray));
    });
    d12simulation.RunInParallel(MONTE_CARLO_TRIALS);
    Console.WriteLine("d12:\n" + SerializeFrequencyTable<int>(d12simulation.Results));

    // ***End main logic***
    //Check for continue
    bool getting = true;
    while(getting)
    {
        System.Console.WriteLine("Roll again?");
        string result=System.Console.ReadLine();
        if (YES.Contains<string>(result))
            getting=false;
        if(NO.Contains<string>(result))
        {
            getting=false;
            running=false;
        }

    }
}
System.Console.WriteLine("Bye!");


