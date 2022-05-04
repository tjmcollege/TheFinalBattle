public class TheUncodedOne : Actor 
{
    public TheUncodedOne() : base("The Uncoded One", Actor.Faction.Baddies, 25, 25, 15, new List<IAction> {new Unraveling()}) 
    {
    }
}