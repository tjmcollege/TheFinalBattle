public class Familiar : Actor 
{

    public Familiar() : base("Familiar", Actor.Faction.Heroes, 10, 10, 5, new List<IAction> {new Bite()}) 
    {
    }
}