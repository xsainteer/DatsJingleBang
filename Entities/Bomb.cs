namespace DatsJingleBang.Entities;

public sealed class Bomb
{
    public Position Position { get; init; }
    public int Radius { get; init; }
    public DateTime ExplodeAt { get; init; }

    public static Bomb Create(Position pos, int radius, int timerSeconds)
        => new Bomb
        {
            Position = pos,
            Radius = radius,
            ExplodeAt = DateTime.UtcNow.AddSeconds(timerSeconds)
        };
}