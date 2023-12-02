using Microsoft.Xna.Framework;

namespace HelloMonoWorld.Game.Entity
{
    public class ZombieEnemy : AbstractEnemy
    {
        public ZombieEnemy(AsepriteDocument aseprite) : base(aseprite)
        {
            this.GetAttribute(Attributes.Attributes.MovementSpeed).BaseValue = 50f;
            this.SetMaxHealth(4f);
            this.OriginalColor = Color.SaddleBrown;
        }

        public override bool Hurt(AbstractEntity source, AbstractEntity directSource, float damage, float knockback = 0)
        {
            if (!base.Hurt(source, directSource, damage, knockback)) 
                return false;
            if (knockback != 0f)
                this.Push(Direction.RIGHT.vector, knockback);
            return true;
        }
    }
}
