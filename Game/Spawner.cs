using HelloMonoWorld.Engine;
using HelloMonoWorld.Game.Entity;
using Microsoft.Xna.Framework;

namespace HelloMonoWorld.Game
{
    public class Spawner : GameObject
    {
        private double _spawnTime;
        private double _minSpawnTime;
        private double _maxSpawnTime;

        public Spawner(double minSpawnTime, double maxSpawnTime)
        {
            //this.SetSprite(Utils.OneByOneTexture);
            this._minSpawnTime = minSpawnTime;
            this._maxSpawnTime = maxSpawnTime;
            this._spawnTime = 0.5d;
            this.SetPosition(new Vector2(Graphics.Width + 80, Graphics.Height / 2f));
        }

        public override void Update()
        {
            base.Update();
            this._spawnTime -= Time.DeltaTime;
            if (this._spawnTime > 0d) 
                return;
        
            this._spawnTime = Mth.NextDouble(MainGame.random, this._minSpawnTime, this._maxSpawnTime);
            this._minSpawnTime -= 0.01f;
            this._maxSpawnTime -= 0.01f;
            ZombieEnemy zombie = new(Sprites.StickmanAnimatedAseprite);
            zombie.SetPosition(this.GetX(), Mth.NextInt(MainGame.random, 100, Graphics.Height - 100));
            Instantiate(zombie);
        }
    }
}
