// See https://aka.ms/new-console-template for more information

using Random_Generator_Mk_2.Generators;
using Random_Generator_Mk_2.Randomizers;
using Random_Generator_Mk_2.Processors;
using Random_Generator_Mk_2.Processors.Math;

string[] YES = { "Y", "y", "yes", "Yes", "YES" };
string[] NO = { "N", "n", "no", "No", "NO" };
string[] ATTRIBUTES = { "STR", "DEX", "CON", "INT", "WIS", "CHA" };

System.Console.WriteLine("D20 Character Creation Dice Rolls");

// ***Begin setup logic***

int sides = 6;
int[] results=new int[sides];
for (int i = 0; i < sides; i++)
{
    results[i] = i + 1;
}
Die<int> die=new Die<int>(results,new SystemRandom());
GeneratorSet<int> fourDice=new GeneratorSet<int>(new Die<int>[] {die,die,die,die});
GeneratorSet<int> threeDice=new DropLowest(fourDice);
Sum sum=new Sum(threeDice);

// ***End setup logic***

bool running=true;
while (running)
{
    
    // ***Begin main logic***

    foreach(string x in ATTRIBUTES)
    {
        Console.WriteLine(x + " " + sum.peek());
    }

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

