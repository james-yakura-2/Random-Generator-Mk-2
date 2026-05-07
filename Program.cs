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
int MONTE_CARLO_TRIALS = 2999000;
Randomizer rng = new SystemRandom();
Aggregation<int> underlying=new MultiGenerator<int>(new Generator<int>[] { });
bool GetYesNo(string prompt)
{
    while (true)
    {
        System.Console.WriteLine(prompt);
        string result = System.Console.ReadLine();
        if (YES.Contains<string>(result))
            return true;
        if (NO.Contains<string>(result))
            return false;
    }
}
int GetInt(string prompt, int min=Int32.MinValue, int max=Int32.MaxValue)
{
    while (true)
    {
        int output = 0;
        System.Console.WriteLine(prompt);
        string result = System.Console.ReadLine();
        try
        {
            output = Convert.ToInt32(result);
            if(min<=output && max >= output)
            {
                return output;
            }
            else
            {
                System.Console.WriteLine("Please enter an integer between " + min + " and " + max + ".");
            }
        }
        catch (FormatException)
        {
            System.Console.WriteLine("Please enter a valid integer.");
        }
    }
}
Die<int>[] createDicePool()
{
    int dice= GetInt("How many dice?");
    int size = GetInt("What size?");
    Die<int>[] diceArray = new Die<int>[dice];
    for (int i = 0; i < diceArray.Length; i++)
    {
        int[] faces = new int[size];
        for (int j = 0; j < size; j++)
            faces[j] = j + 1;
        diceArray[i] = new Die<int>(faces, rng);
    }
    return diceArray;
}
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
        //Result label
        result += "\t" + key.ToString() + "    \t:\t";
        if (dictionary[key] != null)
        {
            //Single result probability
            probability = (float)(dictionary[key]) / MONTE_CARLO_TRIALS;
            result += dictionary[key] + "\t:\t" + probability;
        }
        else
            result += "<NULL>";
        //Aggregate probability
        result += "\t:\t" + (1-total);
        total += probability;
        result += "\t:\t" + total;
        result += "\n";
    }
    result += "}";
    return result;
}
createGenerator<int> setUpGeneratorFactory(bool staticDicePool, int mode)
{

    switch (mode)
    {
        //1. Sum of dice.
        case 1:
            if (staticDicePool)
            {
                Die<int>[] dicepool = createDicePool();
                underlying=new MultiGenerator<int>(dicepool);
                return () => { return new Sum(underlying); };
            }
            else
            {
                return () =>
                {
                    Die<int>[] dicepool = createDicePool();
                    underlying = new MultiGenerator<int>(dicepool);
                    { return new Sum(underlying); };
                };
            }
        //2. Sum of dice, drop lowest.
        case 2:
            if (staticDicePool)
            {
                underlying = new MultiGenerator<int>(createDicePool());
                return () => { return new Sum(new DropLowest(underlying)); };
            }
            else
            {
                return () =>
                {
                    //fix when copy-paste starts working again
                    underlying = new MultiGenerator<int>(createDicePool());
                    { return new Sum(new DropLowest(underlying)); };
                };
            }
        //3. Highest of multiple dice.
        case 3:
            if (staticDicePool)
            {
                underlying = new MultiGenerator<int>(createDicePool());
                return () => { return new Highest(underlying); };
            }
            else
            {
                return () =>
                {
                    underlying = new MultiGenerator<int>(createDicePool());
                    return new Highest(underlying);
                };
            }
        //4. Successes against target number.
        case 4:
            if (staticDicePool)
            {
                underlying = new MultiGenerator<int>(createDicePool());
                int targetNumber = GetInt("What target number?");
                return () => { return new Count<int>(new Exceeds(underlying, targetNumber)); };
            }
            else
            {
                return () =>
                {
                    underlying = new MultiGenerator<int>(createDicePool());
                    int targetNumber = GetInt("What target number?");
                    {
                        return new Count<int>(new Exceeds(underlying, targetNumber));
                    }
                    ;
                };
            }
        //5. Exploding successes against target number.
        case 5:
            if (staticDicePool)
            {
                Die<int>[] dicePool = createDicePool();
                int targetNumber = GetInt("What target number?");
                int explodeNumber= GetInt("What number do you want to explode on?");
                ExplodeIfGE[] explodingDice = new ExplodeIfGE[dicePool.Length];
                for (int i = 0; i < explodingDice.Length; i++)
                    explodingDice[i] = new ExplodeIfGE(dicePool[i], explodeNumber);
                underlying=new Flatten<int>( explodingDice );
                return () => { return new Count<int>(new Exceeds(underlying,targetNumber)); };
            }
            else
            {
                return () =>
                {
                    Die<int>[] dicePool = createDicePool();
                    int targetNumber = GetInt("What target number?");
                    int explodeNumber = GetInt("What number do you want to explode on?");
                    ExplodeIfGE[] explodingDice = new ExplodeIfGE[dicePool.Length];
                    underlying = new Flatten<int>( explodingDice );
                    for (int i = 0; i < explodingDice.Length; i++)
                        explodingDice[i] = new ExplodeIfGE(dicePool[i], explodeNumber);
                    { return new Count<int>(new Exceeds(underlying, targetNumber)); }
                    ;
                };
            }
        //6. Experimental dice mechanic: GEARS.
        case 6:
            if (staticDicePool)
            {
                underlying = new MultiGenerator<int>( createDicePool());
                return () => { return new GearsEscalation(underlying); };
            }
            else
            {
                return () =>
                {
                    underlying = new MultiGenerator<int>(createDicePool());
                    return new GearsEscalation(underlying);
                };

            }
        //7. Stored test: The Walking Dead rolls until botch.
        case 7:
            underlying = new MultiGenerator<int>(new Die<int>[0]);
            if (staticDicePool)
            {
                int dice = GetInt("How many dice?");
                return () => { return new WalkingDeadSimulation(dice, rng); };
            }
            else
            {
                return () => { return new WalkingDeadSimulation(GetInt("How many dice?"), rng); };
            }
        default:
            throw new Exception("Invalid mode selected.");
    }
}


// ***Begin setup logic***

//Get mode
createGenerator<int> generatorFactory=null;

bool staticDicePool = false;
bool monteCarlo = false;
if (GetYesNo("Is this a Monte Carlo simulation?"))
{
    monteCarlo = true;
    staticDicePool = true;
}
else
{
    staticDicePool = GetYesNo("Use the same dice pool for each roll?");
}
int mode = GetInt("Select mode:\n" +
    "1: Sum of dice.\n" +
    "2: Sum of dice, drop lowest.\n" +
    "3: Highest of multiple dice./n" +
    "4: Successes against target number.\n" +
    "5: Exploding successes against target number.\n" +
    "6: Experimental dice mechanic: GEARS.\n" +
    "7: Stored test: The Walking Dead rolls until botch.", 1, 7);
if(staticDicePool && !monteCarlo)
    generatorFactory=setUpGeneratorFactory(staticDicePool, mode);



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

bool running = true;
while (running)
{

    // ***Begin main logic***

    if(!staticDicePool || monteCarlo)
        generatorFactory = setUpGeneratorFactory(staticDicePool, mode);
    if (monteCarlo)
    {
        MonteCarlo<int> simulation = new MonteCarlo<int>(generatorFactory);
        simulation.RunInParallel(MONTE_CARLO_TRIALS);
        Console.WriteLine(SerializeFrequencyTable<int>(simulation.Results));
    }
    else
    {
        Generator<int> generator = generatorFactory();
        Console.WriteLine("Result: " + generator.Peek() + SerializeArray<int>(underlying.Previous));
    }

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

    ////Science Adventure failures
    //System.Console.WriteLine("How many dice?");
    //int dice = Convert.ToInt32(System.Console.ReadLine());
    ////Dictionary<int, int> mapping = new Dictionary<int, int>() 
    ////{ {0, 0 },
    ////{1, 3},
    ////    {2,2},
    ////    {3,2},
    ////    {4,1 },
    ////    {5,0 }
    ////};
    //MonteCarlo<int> d4simulation = new MonteCarlo<int>(() =>
    //{
    //    Die<int>[] diceArray=new Die<int>[dice];
    //    for(int i = 0; i < diceArray.Length; i++)
    //    {
    //        diceArray[i] = new Die<int>(new int[]{ 1,2,3,4 }, rng);
    //    }
    //    return new Highest(new MultiGenerator<int>(diceArray));
    //});
    //d4simulation.RunInParallel(MONTE_CARLO_TRIALS);
    //Console.WriteLine("d4:\n"+SerializeFrequencyTable<int>(d4simulation.Results));
    //MonteCarlo<int> d6simulation = new MonteCarlo<int>(() =>
    //{
    //    Die<int>[] diceArray = new Die<int>[dice];
    //    for (int i = 0; i < diceArray.Length; i++)
    //    {
    //        diceArray[i] = new Die<int>(new int[] { 1, 2, 3, 4, 5, 6 }, rng);
    //    }
    //    return new Highest(new MultiGenerator<int>(diceArray));
    //});
    //d6simulation.RunInParallel(MONTE_CARLO_TRIALS);
    //Console.WriteLine("d6:\n" + SerializeFrequencyTable<int>(d6simulation.Results));
    //MonteCarlo<int> d8simulation = new MonteCarlo<int>(() =>
    //{
    //    Die<int>[] diceArray = new Die<int>[dice];
    //    for (int i = 0; i < diceArray.Length; i++)
    //    {
    //        diceArray[i] = new Die<int>(new int[] { 1, 2, 3, 4, 5, 6,7,8 }, rng);
    //    }
    //    return new Highest(new MultiGenerator<int>(diceArray));
    //});
    //d8simulation.RunInParallel(MONTE_CARLO_TRIALS);
    //Console.WriteLine("d8:\n" + SerializeFrequencyTable<int>(d8simulation.Results));
    //MonteCarlo<int> d10simulation = new MonteCarlo<int>(() =>
    //{
    //    Die<int>[] diceArray = new Die<int>[dice];
    //    for (int i = 0; i < diceArray.Length; i++)
    //    {
    //        diceArray[i] = new Die<int>(new int[] { 1, 2, 3, 4, 5,6,7,8,9,10 }, rng);
    //    }
    //    return new Highest(new MultiGenerator<int>(diceArray));
    //});
    //d10simulation.RunInParallel(MONTE_CARLO_TRIALS);
    //Console.WriteLine("d10:\n" + SerializeFrequencyTable<int>(d10simulation.Results));
    //MonteCarlo<int> d12simulation = new MonteCarlo<int>(() =>
    //{
    //    Die<int>[] diceArray = new Die<int>[dice];
    //    for (int i = 0; i < diceArray.Length; i++)
    //    {
    //        diceArray[i] = new Die<int>(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10,11,12 }, rng);
    //    }
    //    return new Highest(new MultiGenerator<int>(diceArray));
    //});
    //d12simulation.RunInParallel(MONTE_CARLO_TRIALS);
    //Console.WriteLine("d12:\n" + SerializeFrequencyTable<int>(d12simulation.Results));

    //GEARS dice
    //System.Console.WriteLine("How many dice?");
    //int dice = Convert.ToInt32(System.Console.ReadLine());
    //System.Console.WriteLine("What size?");
    //int size = Convert.ToInt32(System.Console.ReadLine());
    //Die<int>[] diceArray = new Die<int>[dice];
    //for (int i = 0; i < diceArray.Length; i++)
    //{
    //    int[] faces = new int[size];
    //    for (int j = 0; j < size; j++)
    //        faces[j] = j + 1;
    //    diceArray[i] = new Die<int>(faces, rng);
    //}
    //MultiGenerator<int> allDice = new MultiGenerator<int>(diceArray);
    //GearsEscalation escalation = new GearsEscalation(allDice);
    //System.Console.WriteLine("Result: " + escalation.Peek()+SerializeArray<int>(allDice.Previous));

    //GEARS Monte Carlo
    //System.Console.WriteLine("How many dice?");
    //int dice = Convert.ToInt32(System.Console.ReadLine());
    //System.Console.WriteLine("What size?");
    //int size = Convert.ToInt32(System.Console.ReadLine());
    //MonteCarlo<int> simulation = new MonteCarlo<int>(() =>
    //{
    //    Die<int>[] diceArray = new Die<int>[dice];
    //    for (int i = 0; i < diceArray.Length; i++)
    //    {
    //        int[] faces = new int[size];
    //        for (int j = 0; j < size; j++)
    //            faces[j] = j + 1;
    //        diceArray[i] = new Die<int>(faces, rng);
    //    }
    //    MultiGenerator<int> allDice = new MultiGenerator<int>(diceArray);
    //    return new GearsEscalation(allDice);
    //});
    //simulation.RunInParallel(MONTE_CARLO_TRIALS);
    //Console.WriteLine(SerializeFrequencyTable<int>(simulation.Results));

    // ***End main logic***
    //Check for continue
    running = GetYesNo("Roll again?");
}
System.Console.WriteLine("Bye!");


