public class BoneCrunch : IAction 
{
    public string Name {get;} = "Bone Crunch";
    public IAction.TargetType Target {get;} = IAction.TargetType.Enemies;

    public void Perform(List<Actor> targets, int targetIndex, Actor performer) 
    {
        Random random = new Random();
        int damage = random.Next(2);
        targets[targetIndex].TakeDamage(damage);
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"{performer.Name.ToUpper()} dealt {damage} damage to {targets[targetIndex].Name.ToUpper()}!");
        Console.ForegroundColor = ConsoleColor.White;
    }
}