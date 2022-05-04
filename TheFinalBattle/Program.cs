Console.WriteLine("Enter the True Programmer's name:");
string heroName = Console.ReadLine();

//Initial hero party.
List<Actor> heroParty = new List<Actor>() {new Hero(heroName), new Familiar()};

//Initial starting items.
List<IAction> heroItems = new List<IAction>() {new HealingPotion()};

//Encounters consist of lists of actors.
List<Actor> encounter1 = new List<Actor>() {new Actor("Skeleton", Actor.Faction.Baddies, 2, 2, 5, new List<IAction> {new BoneCrunch()})};
List<Actor> encounter2 = new List<Actor>() {new Skeleton(), new Skeleton()};
List<Actor> encounter3 = new List<Actor>() {new Skeleton(), new Skeleton(), new Skeleton(), new Skeleton(), new Skeleton(), new TheUncodedOne()};

//List of encounters. Used by the game to ferry the hero party along, fighting each encounter in the list.
List<List<Actor>> encounters = new List<List<Actor>>() {encounter1, encounter2, encounter3};

foreach(List<Actor> encounter in encounters)
{
    Battle currentBattle = new Battle(heroParty, encounter, heroItems);
    currentBattle.Start();

    if(currentBattle.BaddiesWin) 
    {
        Console.WriteLine($"The journey of {heroName} the True Programmer has ended. The denizens of Binaria are doomed");
        Console.WriteLine("to live a programless life. Unless... another hero rises.");
        break;
    }

    heroParty = currentBattle.HeroParty;
    heroItems = currentBattle.HeroItems;
}

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine($"{heroName}, the True Programmer, has defeated the Uncoded One! Binaria is free once again to live");
Console.WriteLine($"a life full of programming! Your legendary feat will forever be enshrined in history.");