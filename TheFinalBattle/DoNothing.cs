public class DoNothing : IAction 
{
    public string Name {get;} = "Do Nothing";
    public IAction.TargetType Target {get;} = IAction.TargetType.Self;
    public void Perform(List<Actor> targets, int targetIndex, Actor performer) 
    {
        Console.WriteLine($"{performer.Name.ToUpper()} did nothing.");
    }
}