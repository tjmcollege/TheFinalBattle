public class Punch : IAction 
{
    public string Name {get;} = "Punch";
    public IAction.TargetType Target {get;} = IAction.TargetType.Other;

    public void Perform(List<Actor> targets, int targetIndex, Actor performer) 
    {
        Random random = new Random();
        int damage = 1;
        targets[targetIndex].TakeDamage(damage);
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"{performer.Name.ToUpper()} dealt {damage} damage to {targets[targetIndex].Name.ToUpper()}!");
        Console.ForegroundColor = ConsoleColor.White;
    }
}