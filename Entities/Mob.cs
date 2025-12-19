namespace DatsJingleBang.Entities;

public sealed class Mob
{
    public Guid Id { get; init; }
    public Position Position { get; set; }
    public MobType Type { get; init; }
    public bool IsSleeping { get; set; } = true;
}