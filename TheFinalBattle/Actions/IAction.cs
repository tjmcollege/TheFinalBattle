public interface IAction 
{
    public string Name {get;}
    public TargetType Target {get;}

    public void Perform(List<Actor> targets, int targetIndex, Actor performer);

    public enum TargetType {Self, Other, Allies, Enemies, Multi};
}