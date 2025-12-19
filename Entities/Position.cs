namespace DatsJingleBang.Entities;

public readonly record struct Position(int X, int Y)
{
    public int DistanceSquared(Position other)
        => (X - other.X) * (X - other.X) + (Y - other.Y) * (Y - other.Y);
}