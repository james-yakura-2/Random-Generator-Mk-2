// See https://aka.ms/new-console-template for more information

using Random_Generator_Mk_2.Generators;
using Random_Generator_Mk_2.Randomizers;
using Random_Generator_Mk_2.Processors;
using Random_Generator_Mk_2.Processors.Aggregations;
using Random_Generator_Mk_2.Processors.Aggregations.Math;

string[] YES = { "Y", "y", "yes", "Yes", "YES" };
string[] NO = { "N", "n", "no", "No", "NO" };
string[] ATTRIBUTES = { "STR", "DEX", "CON", "INT", "WIS", "CHA" };

System.Console.WriteLine("WOD Dice");

// ***Begin setup logic***

// D&D Character Creation
//int sides = 6;
//int[] results=new int[sides];
//for (int i = 0; i < sides; i++)
//{
//    results[i] = i + 1;
//}
//Die<int> die=new Die<int>(results,new SystemRandom());
//Aggregation<int> fourDice=new Aggregation<int>(new Die<int>[] {die,die,die,die});
//Aggregation<int> threeDice=new DropLowest(fourDice);
//Sum sum=new Sum(threeDice);

//World of Darkness die rolls
int sides = 10;
int[] results = new int[sides];
for (int i = 0; i < sides; i++)
{
    results[i] = i + 1;
}
Die<int> die = new Die<int>(results, new SystemRandom());
Exploding<int> explodingDie=new ExplodeIfGE(die,10);


// ***End setup logic***

bool running =true;
while (running)
{

    // ***Begin main logic***

    //D&D Character Creation rolls
    //foreach(string x in ATTRIBUTES)
    //{
    //    Console.WriteLine(x + " " + sum.peek());
    //}

    //World of Darkness die rolls
    //TODO: Set this up to use multiple dice.
    System.Console.WriteLine("How many dice?");
    int dice=Convert.ToInt32(System.Console.ReadLine());
    Aggregation<int>[] explodingDice=new Aggregation<int>[dice];
    for (int i = 0; i < dice; i++)
        explodingDice[i] = explodingDie;

    Flatten<int> flattenedPool=new Flatten<int>(explodingDice);
    Count<int> dicePool = new Count<int>(new Exceeds(flattenedPool, 7));
    int successes = dicePool.peek();
    
    
    System.Console.WriteLine(successes+" successes! "+SerializeArray<int>(flattenedPool.Previous));

    // ***End main logic***
    
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

static string SerializeArray<T>(T[] array)
{
    string result = "[";
    for(int i = 0; i < array.Length; i++)
    {
        result+=array[i].ToString();
        if(i<array.Length-1)
        {
            result += ", ";
        }
    }
    result+="] ("+array.Length+" items)";
    return result;
}
