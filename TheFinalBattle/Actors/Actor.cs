public class Actor 
{
    public string Name {get; protected set;}

    public Faction Team {get; protected set;}
    public int MaxHealth {get; protected set;}

    public int Health {get; protected set;}

    public double InitiativeWeight {get; protected set;}
    public double Initiative {get; protected set;}

    public List<IAction> MoveSet {get; protected set;}

    public Actor(string name, Faction team, int maxHealth, int health, int initiativeWeight, List<IAction> moveSet) 
    {
        Name = name;
        Team = team;
        MaxHealth = maxHealth;
        Health = health;
        InitiativeWeight = initiativeWeight;
        MoveSet = moveSet;
    }

    public void GenerateInitiative(double totalInitiative) 
    {
        double percentInitiative = InitiativeWeight / totalInitiative;
        Random random = new Random();
        Initiative = random.NextDouble() * percentInitiative;
    }

    public bool IsDead() 
    {
        return Health <= 0 ? true : false;
    }

    public void Heal(int regen) 
    {
        Health += regen;
        if(Health > MaxHealth) 
            Health = MaxHealth;
    }

    public void TakeDamage(int damage) 
    {
        Health -= damage;
    }

    public enum Faction {Heroes, Baddies}
}