public class Skeleton : Actor 
{
    public Skeleton() : base("Skeleton", Actor.Faction.Baddies, 5, 5, 2, new List<IAction> {new BoneCrunch()}) 
    {
    }
}
