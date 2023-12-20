using System.Collections.Generic;
using TowerDefense.Entities.Enemies;

namespace TowerDefense.Registry;

public static class EnemiesRegistry
{
    public static Dictionary<string, AbstractEnemy> Enemies = new();
    private static AbstractEnemy Zombie = Register("zombie", new AbstractEnemy(Sprites.GetAnimatedSprite(Sprites.Zombie, "idle"), 1f, 5f));

    private static AbstractEnemy Register(string id, AbstractEnemy abstractEnemy)
    {
        Enemies.Add(id, abstractEnemy);
        return abstractEnemy;
    }

    public static AbstractEnemy CreateFromId(string id)
    {
        return new AbstractEnemy()
    }
}