using Microsoft.Xna.Framework;
using MonoGame.Aseprite.Documents;

namespace HelloMonoWorld.Game.Entity
{
    public class ZombieEnemy : AbstractEnemy
    {
        public ZombieEnemy(AsepriteDocument aseprite) : base(aseprite)
        {
            this.MovementSpeed = 50f;
            this.MaxHealth = 4f;
            this.OriginalColor = Color.SaddleBrown;
        }

        public override void Update()
        {
            base.Update();
        }

        public override bool Hurt(AbstractEntity source, AbstractEntity directSource, float damage, float knockback = 0)
        {
            if (base.Hurt(source, directSource, damage, knockback))
            {
                if (knockback != 0f)
                    this.Push(Direction.RIGHT.vector, knockback);
                return true;
            }
            return false;
        }
    }
}
