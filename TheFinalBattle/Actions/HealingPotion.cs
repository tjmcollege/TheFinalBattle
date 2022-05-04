public class HealingPotion : IAction 
{
    public string Name {get;} = "Healing Potion";
    public IAction.TargetType Target {get;} = IAction.TargetType.Allies;

    public void Perform(List<Actor> targets, int targetIndex, Actor performer) 
    {
        Random random = new Random();
        int regen = 4 + random.Next(2);
        targets[targetIndex].Heal(regen);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"The potion gave {targets[targetIndex].Name.ToUpper()} {regen} points of health!");
        Console.ForegroundColor = ConsoleColor.White;
    }

}