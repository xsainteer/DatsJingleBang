using System.Text.Json;
using System.Text.Json.Serialization;

// Базовые модели команд
public class CommandBomber
{
    [JsonPropertyName("bombs")]
    public List<List<int>> Bombs { get; set; } = new();

    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("path")]
    public List<List<int>> Path { get; set; } = new();
}

public class CommandBooster
{
    [JsonPropertyName("booster")]
    public int BoosterType { get; set; }
}

public class CheatCodeCommand
{
    [JsonPropertyName("code")]
    public string Code { get; set; } = string.Empty;
}

public class PlayerCommand
{
    [JsonPropertyName("bombers")]
    public List<CommandBomber> Bombers { get; set; } = new();
}

// Модели конфигурации
public class BoosterConfig
{
    [JsonPropertyName("limit")]
    public int Limit { get; set; }

    [JsonPropertyName("prices")]
    public List<int> Prices { get; set; } = new();

    [JsonPropertyName("quality")]
    public int Quality { get; set; }
}

public class CheatConfig
{
    [JsonPropertyName("bombersPenalty")]
    public int BombersPenalty { get; set; }

    [JsonPropertyName("cheatCodes")]
    public List<string> CheatCodes { get; set; } = new();

    [JsonPropertyName("cheatCodesEnable")]
    public bool CheatCodesEnable { get; set; }

    [JsonPropertyName("pointBonus")]
    public int PointBonus { get; set; }
}

public class MapConfig
{
    [JsonPropertyName("blocksPerPlayer")]
    public int BlocksPerPlayer { get; set; }

    [JsonPropertyName("crossGap")]
    public int CrossGap { get; set; }

    [JsonPropertyName("crossSize")]
    public int CrossSize { get; set; }

    [JsonPropertyName("densityFactor")]
    public double DensityFactor { get; set; }

    [JsonPropertyName("minSize")]
    public int MinSize { get; set; }

    [JsonPropertyName("mobDistribution")]
    public Dictionary<string, int> MobDistribution { get; set; } = new();

    [JsonPropertyName("mobPercentage")]
    public int MobPercentage { get; set; }

    [JsonPropertyName("padding")]
    public int Padding { get; set; }

    [JsonPropertyName("version")]
    public int Version { get; set; }
}

public class MobConfig
{
    [JsonPropertyName("hysteresis")]
    public int Hysteresis { get; set; }

    [JsonPropertyName("invulnerableDuration")]
    public int InvulnerableDuration { get; set; }

    [JsonPropertyName("maxWanderSteps")]
    public int MaxWanderSteps { get; set; }

    [JsonPropertyName("minWanderSteps")]
    public int MinWanderSteps { get; set; }

    [JsonPropertyName("respawn")]
    public bool Respawn { get; set; }

    [JsonPropertyName("speed")]
    public int Speed { get; set; }

    [JsonPropertyName("vision")]
    public int Vision { get; set; }
}

public class PlayerConfig
{
    [JsonPropertyName("armor")]
    public int Armor { get; set; }

    [JsonPropertyName("bombDelay")]
    public int BombDelay { get; set; }

    [JsonPropertyName("bombers")]
    public int Bombers { get; set; }

    [JsonPropertyName("bombs")]
    public int Bombs { get; set; }

    [JsonPropertyName("canPassBombs")]
    public bool CanPassBombs { get; set; }

    [JsonPropertyName("canPassObstacles")]
    public bool CanPassObstacles { get; set; }

    [JsonPropertyName("canPassWalls")]
    public bool CanPassWalls { get; set; }

    [JsonPropertyName("expectedCount")]
    public int ExpectedCount { get; set; }

    [JsonPropertyName("invulnerableDuration")]
    public int InvulnerableDuration { get; set; }

    [JsonPropertyName("logLimit")]
    public int LogLimit { get; set; }

    [JsonPropertyName("maxPath")]
    public int MaxPath { get; set; }

    [JsonPropertyName("maxSkillPoints")]
    public int MaxSkillPoints { get; set; }

    [JsonPropertyName("range")]
    public int Range { get; set; }

    [JsonPropertyName("speed")]
    public int Speed { get; set; }

    [JsonPropertyName("view")]
    public int View { get; set; }
}

public class SkillConfig
{
    [JsonPropertyName("boosters")]
    public Dictionary<string, BoosterConfig> Boosters { get; set; } = new();

    [JsonPropertyName("deathPenaltyPointsPercentage")]
    public int DeathPenaltyPointsPercentage { get; set; }

    [JsonPropertyName("period")]
    public int Period { get; set; }

    [JsonPropertyName("points")]
    public int Points { get; set; }
}

public class StatsConfig
{
    [JsonPropertyName("kill_points")]
    public int KillPoints { get; set; }

    [JsonPropertyName("mob_kill_points")]
    public int MobKillPoints { get; set; }

    [JsonPropertyName("obstacle_points")]
    public List<int> ObstaclePoints { get; set; } = new();
}

public class GameConfig
{
    [JsonPropertyName("achievements")]
    public List<string> Achievements { get; set; } = new();

    [JsonPropertyName("chainReactionBombCounts")]
    public int ChainReactionBombCounts { get; set; }

    [JsonPropertyName("cheats")]
    public CheatConfig Cheats { get; set; } = new();

    [JsonPropertyName("map")]
    public MapConfig Map { get; set; } = new();

    [JsonPropertyName("mob")]
    public MobConfig Mob { get; set; } = new();

    [JsonPropertyName("player")]
    public PlayerConfig Player { get; set; } = new();

    [JsonPropertyName("printMap")]
    public bool PrintMap { get; set; }

    [JsonPropertyName("respawnDelay")]
    public int RespawnDelay { get; set; }

    [JsonPropertyName("scores")]
    public List<int> Scores { get; set; } = new();

    [JsonPropertyName("seed")]
    public int Seed { get; set; }

    [JsonPropertyName("skill")]
    public SkillConfig Skill { get; set; } = new();

    [JsonPropertyName("spawnAttempts")]
    public int SpawnAttempts { get; set; }

    [JsonPropertyName("stats")]
    public StatsConfig Stats { get; set; } = new();

    [JsonPropertyName("topPlayersGroupSize")]
    public int TopPlayersGroupSize { get; set; }
}

// Модели представления (View)
[JsonConverter(typeof(PositionJsonConverter))]
public record Position(int X, int Y);

public class PositionJsonConverter : JsonConverter<Position>
{
    public override Position Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartArray)
            throw new JsonException();

        reader.Read();
        var x = reader.GetInt32();
        reader.Read();
        var y = reader.GetInt32();
        reader.Read();

        return new Position(x, y);
    }

    public override void Write(Utf8JsonWriter writer, Position value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();
        writer.WriteNumberValue(value.X);
        writer.WriteNumberValue(value.Y);
        writer.WriteEndArray();
    }
}

public class BombView
{
    [JsonPropertyName("pos")]
    public Position Position { get; set; } = new(0, 0);

    [JsonPropertyName("range")]
    public int Range { get; set; }

    [JsonPropertyName("timer")]
    public double Timer { get; set; }
}

public enum BomberTier
{
    Top20,
    Regular
}

[JsonConverter(typeof(BomberTierJsonConverter))]
public class BomberTierJsonConverter : JsonConverter<BomberTier>
{
    public override BomberTier Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        return value?.ToLowerInvariant() switch
        {
            "top_20" => BomberTier.Top20,
            _ => BomberTier.Regular
        };
    }

    public override void Write(Utf8JsonWriter writer, BomberTier value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value switch
        {
            BomberTier.Top20 => "top_20",
            _ => "regular"
        });
    }
}

public class BomberView
{
    [JsonPropertyName("alive")]
    public bool Alive { get; set; }

    [JsonPropertyName("armor")]
    public int Armor { get; set; }

    [JsonPropertyName("bombs_available")]
    public int BombsAvailable { get; set; }

    [JsonPropertyName("can_move")]
    public bool CanMove { get; set; }

    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("pos")]
    public Position Position { get; set; } = new(0, 0);

    [JsonPropertyName("safe_time")]
    public int SafeTime { get; set; }

    [JsonPropertyName("tier")]
    public BomberTier Tier { get; set; }
}

public class EnemyBomberView
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("pos")]
    public Position Position { get; set; } = new(0, 0);

    [JsonPropertyName("safe_time")]
    public int SafeTime { get; set; }

    [JsonPropertyName("tier")]
    public BomberTier Tier { get; set; }
}

public class MobView
{
    public enum MobType
    {
        Ghost,
        Wanderer,
        Chaser
    }

    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("pos")]
    public Position Position { get; set; } = new(0, 0);

    [JsonPropertyName("safe_time")]
    public int SafeTime { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonIgnore]
    public MobType ParsedType => Type?.ToLowerInvariant() switch
    {
        "ghost" => MobType.Ghost,
        "wanderer" => MobType.Wanderer,
        "chaser" => MobType.Chaser,
        _ => MobType.Ghost
    };
}

public class ArenaView
{
    [JsonPropertyName("bombs")]
    public List<BombView> Bombs { get; set; } = new();

    [JsonPropertyName("obstacles")]
    public List<Position> Obstacles { get; set; } = new();

    [JsonPropertyName("walls")]
    public List<Position> Walls { get; set; } = new();
}

public enum BoosterType
{
    Speed,
    BombRange,
    Armor,
    Bombers,
    Bombs
}

public class AvailableBooster
{
    [JsonPropertyName("cost")]
    public int Cost { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonIgnore]
    public BoosterType ParsedType => Type?.ToLowerInvariant() switch
    {
        "speed" => BoosterType.Speed,
        "bomb_range" => BoosterType.BombRange,
        "armor" => BoosterType.Armor,
        "bombers" => BoosterType.Bombers,
        "bombs" => BoosterType.Bombs,
        _ => throw new ArgumentException($"Unknown booster type: {Type}")
    };
}

public class BoosterState
{
    [JsonPropertyName("armor")]
    public int Armor { get; set; }

    [JsonPropertyName("bomb_delay")]
    public int BombDelay { get; set; }

    [JsonPropertyName("bomb_range")]
    public int BombRange { get; set; }

    [JsonPropertyName("bombers")]
    public int Bombers { get; set; }

    [JsonPropertyName("bombs")]
    public int Bombs { get; set; }

    [JsonPropertyName("can_pass_bombs")]
    public bool CanPassBombs { get; set; }

    [JsonPropertyName("can_pass_obstacles")]
    public bool CanPassObstacles { get; set; }

    [JsonPropertyName("can_pass_walls")]
    public bool CanPassWalls { get; set; }

    [JsonPropertyName("points")]
    public int Points { get; set; }

    [JsonPropertyName("speed")]
    public int Speed { get; set; }

    [JsonPropertyName("view")]
    public int View { get; set; }
}

public class AvailableBoosterResponse
{
    [JsonPropertyName("available")]
    public List<AvailableBooster> Available { get; set; } = new();

    [JsonPropertyName("state")]
    public BoosterState State { get; set; } = new();
}

public class BoosterUsage
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("time")]
    public DateTimeOffset Time { get; set; }
}

public class StatCounters
{
    [JsonPropertyName("bomberKillsEnemy")]
    public int BomberKillsEnemy { get; set; }

    [JsonPropertyName("bomberKillsFriendly")]
    public int BomberKillsFriendly { get; set; }

    [JsonPropertyName("bomberKillsSystem")]
    public int BomberKillsSystem { get; set; }

    [JsonPropertyName("bombsDestroyedObstacles_0")]
    public int BombsDestroyedObstacles0 { get; set; }

    [JsonPropertyName("bombsDestroyedObstacles_1")]
    public int BombsDestroyedObstacles1 { get; set; }

    [JsonPropertyName("bombsDestroyedObstacles_2")]
    public int BombsDestroyedObstacles2 { get; set; }

    [JsonPropertyName("bombsDestroyedObstacles_3")]
    public int BombsDestroyedObstacles3 { get; set; }

    [JsonPropertyName("bombsPlaced")]
    public int BombsPlaced { get; set; }

    [JsonPropertyName("boosters")]
    public string Boosters { get; set; } = string.Empty;

    [JsonPropertyName("distance")]
    public int Distance { get; set; }

    [JsonPropertyName("kills")]
    public int Kills { get; set; }

    [JsonPropertyName("mobKills")]
    public int MobKills { get; set; }

    [JsonPropertyName("multiKillEnemy")]
    public int MultiKillEnemy { get; set; }

    [JsonPropertyName("multiKillFriendly")]
    public int MultiKillFriendly { get; set; }

    [JsonPropertyName("multiKillSystem")]
    public int MultiKillSystem { get; set; }

    [JsonPropertyName("obstaclesDestroyed")]
    public int ObstaclesDestroyed { get; set; }

    [JsonPropertyName("pointsFromKills")]
    public int PointsFromKills { get; set; }

    [JsonPropertyName("pointsFromMobKills")]
    public int PointsFromMobKills { get; set; }

    [JsonPropertyName("pointsFromObstacles")]
    public int PointsFromObstacles { get; set; }

    [JsonPropertyName("pointsLostToRespawn")]
    public int PointsLostToRespawn { get; set; }

    [JsonPropertyName("rawScore")]
    public int RawScore { get; set; }

    [JsonPropertyName("respawns")]
    public int Respawns { get; set; }

    [JsonIgnore]
    public Dictionary<int, int> BombsDestroyedObstaclesDistribution => new()
    {
        { 0, BombsDestroyedObstacles0 },
        { 1, BombsDestroyedObstacles1 },
        { 2, BombsDestroyedObstacles2 },
        { 3, BombsDestroyedObstacles3 }
    };
}

public class PlayerInfoResponse
{
    [JsonPropertyName("invulnerableDuration")]
    public int InvulnerableDuration { get; set; }

    [JsonPropertyName("player")]
    public string Player { get; set; } = string.Empty;

    [JsonPropertyName("state")]
    public BoosterState State { get; set; } = new();

    [JsonPropertyName("stats")]
    public StatCounters Stats { get; set; } = new();

    [JsonPropertyName("tier")]
    public string Tier { get; set; } = string.Empty;
}

public class PlayerResponse
{
    [JsonPropertyName("arena")]
    public ArenaView Arena { get; set; } = new();

    [JsonPropertyName("bombers")]
    public List<BomberView> Bombers { get; set; } = new();

    [JsonPropertyName("code")]
    public int Code { get; set; }

    [JsonPropertyName("enemies")]
    public List<EnemyBomberView> Enemies { get; set; } = new();

    [JsonPropertyName("errors")]
    public List<string> Errors { get; set; } = new();

    [JsonPropertyName("map_size")]
    public Position MapSize { get; set; } = new(0, 0);

    [JsonPropertyName("mobs")]
    public List<MobView> Mobs { get; set; } = new();

    [JsonPropertyName("player")]
    public string Player { get; set; } = string.Empty;

    [JsonPropertyName("raw_score")]
    public int RawScore { get; set; }

    [JsonPropertyName("round")]
    public string Round { get; set; } = string.Empty;
}

public class CheatCodeResponse
{
    [JsonPropertyName("code")]
    public string Code { get; set; } = string.Empty;

    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;
}

// Модели логов
public class LogMessage
{
    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;

    [JsonPropertyName("time")]
    public DateTimeOffset Time { get; set; }
}

// Модели для наблюдателя (Observer)
public class ObserverPlayer
{
    [JsonPropertyName("bomberKillsEnemy")]
    public int BomberKillsEnemy { get; set; }

    [JsonPropertyName("bomberKillsFriendly")]
    public int BomberKillsFriendly { get; set; }

    [JsonPropertyName("bomberKillsSystem")]
    public int BomberKillsSystem { get; set; }

    [JsonPropertyName("bombers")]
    public List<BomberView> Bombers { get; set; } = new();

    [JsonPropertyName("bombsDestroyedObstacles")]
    public List<int> BombsDestroyedObstacles { get; set; } = new();

    [JsonPropertyName("bombsPlaced")]
    public int BombsPlaced { get; set; }

    [JsonPropertyName("boosters")]
    public List<BoosterUsage> Boosters { get; set; } = new();

    [JsonPropertyName("distance")]
    public int Distance { get; set; }

    [JsonPropertyName("kills")]
    public int Kills { get; set; }

    [JsonPropertyName("mobKills")]
    public int MobKills { get; set; }

    [JsonPropertyName("multiKillEnemy")]
    public int MultiKillEnemy { get; set; }

    [JsonPropertyName("multiKillFriendly")]
    public int MultiKillFriendly { get; set; }

    [JsonPropertyName("multiKillSystem")]
    public int MultiKillSystem { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("obstaclesDestroyed")]
    public int ObstaclesDestroyed { get; set; }

    [JsonPropertyName("pointsFromKills")]
    public int PointsFromKills { get; set; }

    [JsonPropertyName("pointsFromMobKills")]
    public int PointsFromMobKills { get; set; }

    [JsonPropertyName("pointsFromObstacles")]
    public int PointsFromObstacles { get; set; }

    [JsonPropertyName("pointsLostToRespawn")]
    public int PointsLostToRespawn { get; set; }

    [JsonPropertyName("rawScore")]
    public int RawScore { get; set; }

    [JsonPropertyName("respawns")]
    public int Respawns { get; set; }

    [JsonPropertyName("score")]
    public int Score { get; set; }
}

public class ObserverPlayers
{
    [JsonPropertyName("players")]
    public List<ObserverPlayer> Players { get; set; } = new();

    [JsonPropertyName("realmName")]
    public string RealmName { get; set; } = string.Empty;
}

public class ObserverMap
{
    [JsonPropertyName("arena")]
    public ArenaView Arena { get; set; } = new();

    [JsonPropertyName("config")]
    public GameConfig Config { get; set; } = new();

    [JsonPropertyName("realmName")]
    public string RealmName { get; set; } = string.Empty;

    [JsonPropertyName("size")]
    public Position Size { get; set; } = new(0, 0);
}

public class ObserverMobs
{
    [JsonPropertyName("mobs")]
    public List<MobView> Mobs { get; set; } = new();

    [JsonPropertyName("realmName")]
    public string RealmName { get; set; } = string.Empty;
}

// Статистические модели
public class Achievement
{
    [JsonPropertyName("counters")]
    public Dictionary<string, double> Counters { get; set; } = new();

    [JsonPropertyName("createdAt")]
    public DateTimeOffset CreatedAt { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("player")]
    public string Player { get; set; } = string.Empty;

    [JsonPropertyName("realm")]
    public string Realm { get; set; } = string.Empty;
}

public class PlayerStats
{
    [JsonPropertyName("attempt")]
    public int Attempt { get; set; }

    [JsonPropertyName("counters")]
    public Dictionary<string, double> Counters { get; set; } = new();

    [JsonPropertyName("endedAt")]
    public DateTimeOffset EndedAt { get; set; }

    [JsonPropertyName("player")]
    public string Player { get; set; } = string.Empty;

    [JsonPropertyName("score")]
    public double Score { get; set; }
}

public class RealmStats
{
    [JsonPropertyName("game")]
    public string Game { get; set; } = string.Empty;

    [JsonPropertyName("players")]
    public List<PlayerStats> Players { get; set; } = new();

    [JsonPropertyName("realm")]
    public string Realm { get; set; } = string.Empty;

    [JsonPropertyName("startedAt")]
    public DateTimeOffset StartedAt { get; set; }
}

public class TotalStats
{
    [JsonPropertyName("players")]
    public List<PlayerStats> Players { get; set; } = new();
}

public class RealmStatsResponse
{
    [JsonPropertyName("achieves")]
    public List<Achievement> Achievements { get; set; } = new();

    [JsonPropertyName("realms")]
    public Dictionary<string, RealmStats> Realms { get; set; } = new();

    [JsonPropertyName("total")]
    public TotalStats Total { get; set; } = new();
}

// Модели раундов
public enum RoundStatus
{
    Active,
    Finished,
    Pending
}

[JsonConverter(typeof(RoundStatusJsonConverter))]
public class RoundStatusJsonConverter : JsonConverter<RoundStatus>
{
    public override RoundStatus Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        return value?.ToLowerInvariant() switch
        {
            "active" => RoundStatus.Active,
            "finished" => RoundStatus.Finished,
            "pending" => RoundStatus.Pending,
            _ => throw new JsonException($"Unknown round status: {value}")
        };
    }

    public override void Write(Utf8JsonWriter writer, RoundStatus value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString().ToLowerInvariant());
    }
}

public class RoundResponse
{
    [JsonPropertyName("duration")]
    public int Duration { get; set; }

    [JsonPropertyName("endAt")]
    public DateTimeOffset EndAt { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("repeat")]
    public int Repeat { get; set; }

    [JsonPropertyName("startAt")]
    public DateTimeOffset StartAt { get; set; }

    [JsonPropertyName("status")]
    public RoundStatus Status { get; set; }
}

public class RoundListResponse
{
    [JsonPropertyName("eventId")]
    public string EventId { get; set; } = string.Empty;

    [JsonPropertyName("now")]
    public DateTimeOffset Now { get; set; }

    [JsonPropertyName("rounds")]
    public List<RoundResponse> Rounds { get; set; } = new();
}

// Модель ошибки
public class PublicError
{
    [JsonPropertyName("code")]
    public int Code { get; set; }

    [JsonPropertyName("errors")]
    public List<string> Errors { get; set; } = new();
}