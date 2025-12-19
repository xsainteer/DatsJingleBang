namespace DatsJingleBang.Entities;

public sealed class Unit
{
    public Guid Id { get; init; }
    public Position Position { get; set; }

    public int Speed { get; set; } = GameConstants.InitialSpeed;
    public int VisionRadius { get; set; } = GameConstants.InitialVisionRadius;
    public int Armor { get; set; } = 0;

    public int MaxBombs { get; set; } = GameConstants.InitialBombs;
    public int AvailableBombs { get; set; } = GameConstants.InitialBombs;
}