using HelloMonoWorld.Engine;
using HelloMonoWorld.Game.Entity;
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

        public Spawner(double minSpawnTime, double maxSpawnTime) : base()
        {
            //this.SetSprite(Utils.OneByOneTexture);
            this.minSpawnTime = minSpawnTime;
            this.maxSpawnTime = maxSpawnTime;
            spawnTime = 1d / 60d;//Mth.NextDouble(MainGame.random, minSpawnTime, maxSpawnTime);
            this.SetPosition(new Vector2(Graphics.Width + 80, Graphics.Height / 2));
        }

        public override void Update()
        {
            base.Update();
            if (spawnTime > 0d)
            {
                spawnTime -= Time.DeltaTime;
                if (spawnTime <= 0d)
                {
                    spawnTime = Mth.NextDouble(MainGame.random, minSpawnTime, maxSpawnTime);
                    ZombieEnemy zombie = new(Sprites.StickmanAnimatedAseprite);
                    zombie.SetPosition(this.GetX(), Mth.NextInt(MainGame.random, 100, Graphics.Height - 100));
                    Instantiate(zombie);
                }
            }
        }
    }
}
