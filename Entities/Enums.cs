namespace DatsJingleBang.Entities;

public enum CellType
{
    Empty,
    SolidObstacle,
    DestructibleObstacle,
    Bomb,
    Unit,
    Mob
}

public enum MobType
{
    Patrol,
    Ghost
}

public enum UpgradeType
{
    BombCapacity,
    BombRadius,
    Speed,
    Vision,
    ExtraUnit,
    Armor,
    BombFuse,
    Acrobatics
}
