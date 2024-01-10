using System;
using System.Collections.Generic;
using System.Linq;
using TowerDefense.Entities.Enemies;

namespace TowerDefense.Registry;

public static partial class EnemiesRegistry
{
    //public static Dictionary<string, Func<AbstractEnemy>> Enemies = new();
    public static readonly List<RegistryObject<AbstractEnemy>> Enemies = new();
    public static readonly RegistryObject<AbstractEnemy> Zombie = Register("zombie", () => new GenericMovingEnemy(Sprites.GetAnimatedSprite(Sprites.Zombie, "idle")));
    //public static readonly Func<AbstractEnemy> Spider = Register("spider", () => new GenericMovingEnemy(Sprites.GetAnimatedSprite(Sprites.Zombie, "idle")));

    private static RegistryObject<AbstractEnemy> Register(string id, Func<AbstractEnemy> abstractEnemy)
    {
        RegistryObject<AbstractEnemy> registryObject = new(id, abstractEnemy);
        Enemies.Add(registryObject);
        return registryObject;
    }

    public static RegistryObject<AbstractEnemy> GetFromId(string id)
    {
        return Enemies.FirstOrDefault(registryObject => registryObject.Id.Equals(id));
    }
}