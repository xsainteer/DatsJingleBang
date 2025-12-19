namespace DatsJingleBang;

public static class GameConstants
{
    // World
    public const int TickMs = 50;

    // Units
    public const int InitialUnits = 6;
    public const int InitialSpeed = 2;
    public const int InitialVisionRadius = 5;
    public const int MaxPathLength = 30;
    public const int RespawnInvulnerabilitySeconds = 5;
    public const double RespawnPenaltyPercent = 0.10;

    // Bombs
    public const int InitialBombs = 1;
    public const int InitialBombRadius = 1;
    public const int InitialBombTimerSeconds = 8;
    public const int BombTimerReduction = 2;
    public const int MaxBombTimerUpgrades = 3;

    // Skill points
    public const int SkillPointIntervalSeconds = 90;
    public const int MaxSkillPointsPerRound = 10;

    // Scoring
    public const int ScoreDestroyObstacle = 1;
    public const int ScoreKillUnit = 10;
    public const int ScoreKillMob = 10;

    // Vision
    public const int VisionUpgradeBonus = 3;

    // Limits
    public const int RequestsPerSecond = 3;
}