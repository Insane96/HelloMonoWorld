using Microsoft.Xna.Framework;
using MonoEngine;
using TowerDefense.Entities.Enemies;
using TowerDefense.Registry;

namespace TowerDefense.Entities;

public class StartingPoint : GameObject
{
    public double TimeTillNextSpawn = 7.5f;
    public int Spawned;
    public override void Update()
    {
        this.TimeTillNextSpawn -= Time.DeltaTime;
        if (this.TimeTillNextSpawn <= 0f)
        {
            this.TimeTillNextSpawn = 2f;
            AbstractEnemy abstractEnemy = EnemiesRegistry.Zombie.Create();
            abstractEnemy.Position = new Vector2(this.Position.X, this.Position.Y);
            abstractEnemy.MaxHealth = ++this.Spawned * 0.3f;
            GameObject.Instantiate(abstractEnemy);
        }
    }
}