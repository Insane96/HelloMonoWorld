using HelloMonoWorld.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloMonoWorld
{
    public class Spawner : GameObject
    {
        private double spawnTime;
        private double minSpawnTime;
        private double maxSpawnTime;

        private int spawnId = 0;

        public Spawner(double minSpawnTime, double maxSpawnTime) : base("spawner")
        {
            this.minSpawnTime = minSpawnTime;
            this.maxSpawnTime = maxSpawnTime;
            this.spawnTime = 1d / 60d;//Mth.NextDouble(MainGame.random, minSpawnTime, maxSpawnTime);
            this.position = new Vector2(Graphics.UnscaledWidth + 100, Graphics.UnscaledHeight / 2);
        }

        public override void Update()
        {
            if (this.spawnTime > 0d)
            {
                this.spawnTime -= Time.DeltaTime;
                if (this.spawnTime <= 0d)
                {
                    this.spawnTime = Mth.NextDouble(MainGame.random, this.minSpawnTime, this.maxSpawnTime);
                    ZombieEnemy zombie = new($"zombie{spawnId++}", "stickman")
                    {
                        position = new Vector2(this.position.X, Mth.NextInt(MainGame.random, 100, Graphics.ScreenHeight - 100)),
                    };
                    Engine.Engine.Instantiate(zombie);
                }
            }
        }
    }
}
