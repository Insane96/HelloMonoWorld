using Newtonsoft.Json;

namespace TowerDefense.Data;

public class Enemy
{
    [JsonProperty("enemy_id")]
    public string EnemyId { get; private set; }
    [JsonProperty("base_hp")]
    public float BaseHp { get; private set; }
    [JsonProperty("should_scale_with_wave")]
    public bool ShouldScaleWithWave { get; private set; }
    
}