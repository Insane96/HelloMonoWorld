using Engine;

namespace TBRpg.Entities;

public class Entity : GameObject
{
    public int MaxHealth { get; set; }
    public int Health { get; set; }
}