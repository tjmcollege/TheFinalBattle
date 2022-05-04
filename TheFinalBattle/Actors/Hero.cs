public class Hero : Actor 
{

    public Hero(string name) : base(name, Actor.Faction.Heroes, 25, 25, 10, new List<IAction> {new Punch(), new ProgrammersRage()}) 
    {
        Name = name;
    }
}
