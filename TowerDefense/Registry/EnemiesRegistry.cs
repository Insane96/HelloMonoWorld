using System;
using System.Collections.Generic;
using TowerDefense.Entities.Enemies;

namespace TowerDefense.Registry;

public static class EnemiesRegistry
{
    public static Dictionary<string, Func<AbstractEnemy>> Enemies = new();
    public static readonly Func<AbstractEnemy> Zombie = Register("zombie", new GenericMovingEnemy(Sprites.GetAnimatedSprite(Sprites.Zombie, "idle")));
    public static readonly Func<AbstractEnemy> Spider = Register("spider", new GenericMovingEnemy(Sprites.GetAnimatedSprite(Sprites.Zombie, "idle")));

    private static Func<AbstractEnemy> Register(string id, AbstractEnemy abstractEnemy)
    {
        Enemies.Add(id, () => abstractEnemy);
        return () => abstractEnemy;
    }

    public static AbstractEnemy CreateFromId(string id)
    {
        return !Enemies.TryGetValue(id, out Func<AbstractEnemy> enemy) ? null : enemy.Invoke();
    }
}