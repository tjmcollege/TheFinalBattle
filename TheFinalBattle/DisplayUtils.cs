public static class DisplayUtils 
{

    public static void Battle(List<Actor> actors, int currentIndex) 
    {
        Console.WriteLine("------------------------------");
        for(int i = 0; i < actors.Count; i++) 
        {
            if(i == currentIndex) 
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(">");
            }

            if(actors[i].Team == Actor.Faction.Heroes) 
                Console.WriteLine($"({actors[i].Health}/{actors[i].MaxHealth}) {actors[i].Name.ToUpper()}");
            else 
                Console.WriteLine($"                ({actors[i].Health}/{actors[i].MaxHealth}) {actors[i].Name.ToUpper()}");

            Console.ForegroundColor = ConsoleColor.White;
        }
    }

    public static void Targets(List<Actor> targets) 
    {
        Console.WriteLine("------------------------------");
        Console.WriteLine("Choose a target.");
        for(int i = 0; i < targets.Count; i++) 
        {
            Console.WriteLine($"{i}: {targets[i].Name}");
        }
    }

    public static void Actions(List<IAction> actions, string message) 
    {
        Console.WriteLine("------------------------------");
        Console.WriteLine(message);
        for(int i = 0; i < actions.Count; i++) 
        {
            Console.WriteLine($"{i}: {actions[i]}");
        }
    }
}