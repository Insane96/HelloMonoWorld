using HelloMonoWorld.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloMonoWorld.Game
{
    public class Spawner : GameObject
    {
        private double spawnTime;
        private double minSpawnTime;
        private double maxSpawnTime;

        private int spawnId = 0;

        public Spawner(double minSpawnTime, double maxSpawnTime) : base()
        {
            this.minSpawnTime = minSpawnTime;
            this.maxSpawnTime = maxSpawnTime;
            spawnTime = 1d / 60d;//Mth.NextDouble(MainGame.random, minSpawnTime, maxSpawnTime);
            Position = new Vector2(Graphics.Width + 80, Graphics.Height / 2);
        }

        public override void Update()
        {
            if (spawnTime > 0d)
            {
                spawnTime -= Time.DeltaTime;
                if (spawnTime <= 0d)
                {
                    spawnTime = Mth.NextDouble(MainGame.random, minSpawnTime, maxSpawnTime);
                    ZombieEnemy zombie = new($"zombie{spawnId++}", "stickman")
                    {
                        Position = new Vector2(Position.X, Mth.NextInt(MainGame.random, 100, Graphics.Height - 100)),
                    };
                    Instantiate(zombie);
                }
            }
        }
    }
}
