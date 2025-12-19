using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DatsJingleBang.Entities;
public class Arena
{
    [JsonPropertyName("bombs")]
    public List<Bomb> Bombs { get; init; } = [];

    [JsonPropertyName("obstacles")]
    public List<List<int>> Obstacles { get; set; }

    [JsonPropertyName("walls")]
    public List<List<int>> Walls { get; set;} 
}
