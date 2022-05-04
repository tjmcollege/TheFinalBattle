public class Battle 
{

    public List<Actor> HeroParty {get; private set;}
    public List<Actor> Encounter {get; private set;}

    public List<IAction> HeroItems {get; private set;}

    public bool HeroesWin {get; set;} = false;
    public bool BaddiesWin {get; set;} = false;

    public Battle(List<Actor> heroParty, List<Actor> encounter, List<IAction> heroItems) 
    {
        HeroParty = heroParty;
        Encounter = encounter;
        HeroItems = heroItems;
    }

    //If this class seems mangled its because it was originally the main program
    public void Start()
    {

        List<Actor> allActors = new List<Actor> {};
        allActors.AddRange(HeroParty);
        allActors.AddRange(Encounter);

        allActors = OrderByInitiative(allActors);

        int i = 0;

        while(i < allActors.Count() && (!(HeroesWin || BaddiesWin)))
        {
            Actor actor = allActors[i];

            if(actor.Team == Actor.Faction.Heroes)
                allActors = PlayerTurn(allActors, actor, i);
            if(actor.Team == Actor.Faction.Baddies)
                allActors = RandomTurn(allActors, actor, i);

            for(int j = 0; j < allActors.Count; j++) 
            {
                if(allActors[j].IsDead()) 
                {
                    Console.WriteLine($"{allActors[j].Name.ToUpper()} has perished!");
                    Console.WriteLine();
                    allActors.RemoveAt(j);
                }
            }

            if(CheckHeroesWin(allActors))
                HeroesWin = true;
            if(CheckBaddiesWin(allActors))
                BaddiesWin = true;

            i++;

            if(i == allActors.Count())
                i = 0;
        }
    }

    List<Actor> PlayerTurn(List<Actor> actors, Actor actor, int currentIndex)
    {
        int moveSetIndex;
        int selfIndex = ActorIndex(actors, actor);
        int itemIndex;
        int targetIndex;

        Console.WriteLine($"It is {actor.Name.ToUpper()}'s turn...");

        DisplayUtils.Battle(actors, currentIndex);

        if(HeroItems.Count > 0) 
        {
            Console.WriteLine("Use an item?"); 
            if (Console.ReadLine() == "y") 
            {
                DisplayUtils.Actions(HeroItems,"Choose an item.");
                itemIndex = Convert.ToInt32(Console.ReadLine());

                targetIndex = ReturnTargetIndex(actor.MoveSet[itemIndex]);

                HeroItems[itemIndex].Perform(actors, targetIndex, actor);

                //I probably should make heroItems its own class. Currently, items can only be used once per turn.
                HeroItems.RemoveAt(itemIndex);
            }
        }

        DisplayUtils.Actions(actor.MoveSet,"Choose an action.");
        moveSetIndex = Convert.ToInt32(Console.ReadLine());

        targetIndex = ReturnTargetIndex(actor.MoveSet[moveSetIndex]);

        actor.MoveSet[moveSetIndex].Perform(actors, targetIndex, actor);
        Console.WriteLine();

        int ReturnTargetIndex(IAction action) 
        {
            if (action.Target == IAction.TargetType.Multi) 
            {

            }
             //Since I made targeting index based, getting a list of allies means different indices, which means making my life harder
            else if (action.Target == IAction.TargetType.Enemies) 
            {
                List<Actor> enemyActors = new List<Actor>();
                List<int> trueIndex = new List<int>();

                //reaching into global variables is fun for the whole family
                for(int i = 0; i < actors.Count; i++) 
                {
                    if(actors[selfIndex].Team != actors[i].Team) 
                    {
                        enemyActors.Add(actors[i]);
                        trueIndex.Add(ActorIndex(actors, actors[i]));
                    }
                }
                DisplayUtils.Targets(enemyActors);
                return trueIndex[Convert.ToInt32(Console.ReadLine())];
            }
            else if(action.Target == IAction.TargetType.Allies)
            {
                List<Actor> alliedActors = new List<Actor>();
                List<int> trueIndex = new List<int>();

                for(int i = 0; i < actors.Count; i++) 
                {
                    if(actors[selfIndex].Team == actors[i].Team) 
                    {
                        alliedActors.Add(actors[i]);
                        trueIndex.Add(ActorIndex(actors, actors[i]));
                    }
                }
                DisplayUtils.Targets(alliedActors);
                return trueIndex[Convert.ToInt32(Console.ReadLine())];
            }
            else if(action.Target == IAction.TargetType.Self)
                return selfIndex;
            else
                DisplayUtils.Targets(actors);
            return Convert.ToInt32(Console.ReadLine());
        }

        return actors;
    }

    List<Actor> RandomTurn(List<Actor> actors, Actor actor, int currentIndex)
    {
        Random random = new Random();
        int moveSetIndex = random.Next(actor.MoveSet.Count);
        int selfIndex = ActorIndex(actors, actor);
        int targetIndex;
        int randomTarget;

        Console.WriteLine($"It is {actor.Name.ToUpper()}'s turn...");

        if (actor.MoveSet[moveSetIndex].Target == IAction.TargetType.Self) 
        {
            targetIndex = selfIndex;
        }
        //code duplication :)
        else if (actor.MoveSet[moveSetIndex].Target == IAction.TargetType.Enemies) 
        {
            List<Actor> enemyActors = new List<Actor>();
            List<int> trueIndex = new List<int>();

            for(int i = 0; i < actors.Count; i++) 
            {
                if(actors[selfIndex].Team != actors[i].Team) 
                {
                    enemyActors.Add(actors[i]);
                    trueIndex.Add(ActorIndex(actors, actors[i]));
                }
            }
            targetIndex = trueIndex[random.Next(enemyActors.Count)];
        }
        else 
        {
            //Ensures the computer cannot target itself by randomly generating until its target index is not its own index lol
            //Note: Would be very bad if this ran when allActors has 1 member
            //Or I could avoid all this if I had an action TYPE for self, and one for others ;)
            do
            {
                randomTarget = random.Next(actors.Count);
            }
            while(randomTarget == selfIndex);
            targetIndex = randomTarget;
        }

        actor.MoveSet[moveSetIndex].Perform(actors, targetIndex, actor);
        Console.WriteLine();

        return actors;
    }

    int ActorIndex(List<Actor> actors, Actor actor) 
    {
        for(int i = 0; i < actors.Count; i++) 
        {
            if(actors[i] == actor)
                return i;
        };
        throw new Exception("Actor could not find self in List<Actor>.");
    }

    List<Actor> OrderByInitiative(List<Actor> actors) 
    {
        double totalInitiativeWeight = 0;

        foreach (Actor actor in actors)
            totalInitiativeWeight += actor.InitiativeWeight;

        foreach (Actor actor in actors)
            actor.GenerateInitiative(totalInitiativeWeight);
        
        //Decides turn order in a battle
        IEnumerable<Actor> actorsInitiativeDescending = from a in actors
                                                        orderby a.Initiative descending
                                                        select a;

        return actorsInitiativeDescending.ToList<Actor>();

    }

    bool CheckHeroesWin(List<Actor> actors) 
    {
        int count = 0;

        foreach(Actor actor in actors) 
        {
            if(Actor.Faction.Heroes != actor.Team)
                count++;
        }

        return count == 0 ? true : false;
    }

    bool CheckBaddiesWin(List<Actor> actors) 
    {
        int count = 0;

        foreach(Actor actor in actors) 
        {
            if(Actor.Faction.Baddies != actor.Team)
                count++;
        }

        return count == 0 ? true : false;
    }
        
}