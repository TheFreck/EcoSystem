using EcoSystem;

var gettingHawks = true;
var hawks = 0;
var doves = 0;
var generations = 0;
var creatures = new List<Creature>();
var randy = new Random();

do
{
    Console.WriteLine("How many Hawks?");
    var hawksIn = Console.ReadLine();
    if(int.TryParse(hawksIn, out hawks))
    {
        gettingHawks = false;
        for(var i=0; i<hawks; i++)
        {
            creatures.Add(new Creature(Species.Hawk,"Hawk-" + i));
        }
    }
    else
    {
        Console.WriteLine("Please enter an integer");
    }
} while (gettingHawks);

var gettingDoves = true;

do
{
    Console.WriteLine("How many Doves?");
    var dovesIn = Console.ReadLine();
    if (int.TryParse(dovesIn, out doves))
    {
        gettingDoves = false;
        for(var i=0; i < doves; i++)
        {
            creatures.Add(new Creature(Species.Dove,"Dove-" + i));
        }
    }
    else
    {
        Console.WriteLine("Please enter an integer");
    }
} while (gettingDoves);

var gettingGenerations = true;

do
{
    Console.WriteLine("How many generations will we run?");
    var gensIn = Console.ReadLine();
    if(int.TryParse(gensIn, out generations))
    {
        gettingGenerations = false;
    }
    else
    {
        Console.WriteLine("Please enter an integer");
    }
} while (gettingGenerations);

var ecoService = new EcoService(creatures);
var generationsArray = new Generation[generations];
for(var i=0; i<generations; i++)
{
    if (i % 20 == 19)
    {
        generationsArray[i] = ecoService.Reproduce();
        Console.WriteLine($"={i}====================");
        Console.WriteLine("Births: " + generationsArray[i].BirthCount);
        Console.WriteLine("  Hawks: " + generationsArray[i].Births.Where(b => b.Species == Species.Hawk).ToList().Count);
        Console.WriteLine("  Doves: " + generationsArray[i].Births.Where(b => b.Species == Species.Dove).ToList().Count);
        Console.WriteLine("=====================");
        Console.WriteLine("Deaths: " + generationsArray[i].DeathCount);
        Console.WriteLine("  Hawks: " + generationsArray[i].Deaths.Where(b => b.Species == Species.Hawk).ToList().Count);
        Console.WriteLine("  Doves: " + generationsArray[i].Deaths.Where(b => b.Species == Species.Dove).ToList().Count);
        Console.WriteLine("=====================");
        Console.WriteLine("Population: " + generationsArray[i].Creatures.Count);
        Console.WriteLine("  Hawks: " + generationsArray[i].Creatures.Where(b => b.Species == Species.Hawk).ToList().Count);
        Console.WriteLine("  Doves: " + generationsArray[i].Creatures.Where(b => b.Species == Species.Dove).ToList().Count);
        Console.WriteLine("=====================");
        Console.WriteLine();
    }
    else
    {
        generationsArray[i] = ecoService.NextGeneration(ecoService.PairCreatures(creatures,randy));
    }
}
    Console.WriteLine("================");