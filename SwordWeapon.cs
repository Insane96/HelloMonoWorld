using HelloMonoWorld.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace HelloMonoWorld;

//Legacy code when you could use a sword to attack
public class SwordWeapon : Entity
{
    public double damage;
    public double attackSpeed;
    public Entity wielder;

    public SwordWeapon(string id, string spriteName, double damage, double attackSpeed, Entity wielder) : base(id, spriteName)
    {
        this.damage = damage;
        this.attackSpeed = attackSpeed;
        this.wielder = wielder;
        this.movementSpeed = 0f;
        this.ShouldUpdateBounds = false;
        this.Hide();
        this.Disable();
    }

    public override void Update()
    {
        FollowWielder();
    }

    public void Attack()
    {
        this.FollowWielder();
        this.Show();
        this.Enable();
        this.GetAttackSound().Play(0.5f * Options.Volume, Mth.NextFloat(MainGame.random, -0.25f, 0.25f), 0f);
        /*if (this.wielder.attackDirection.Equals(Direction.RIGHT))
        {
            this.origin = new(0f, 0.5f);
            this.spriteEffect = SpriteEffects.None;
        }
        else
        {
            this.origin = new(1f, 0.5f);
            this.spriteEffect = SpriteEffects.FlipHorizontally;
        }*/
        this.UpdateBounds();
        List<Entity> entitiesCollided = this.GetCollisions(this.wielder);
        entitiesCollided.ForEach(entity => entity.Hurt(this.damage, this.attackSpeed));
    }

    private void FollowWielder()
    {
        /*Vector2 weaponPos = this.wielder.attackDirection == Direction.RIGHT ? this.wielder.LeftHand : this.wielder.RightHand;
        this.position = new Vector2(this.wielder.position.X - (this.wielder.texture.Width * this.wielder.origin.X) + weaponPos.X, this.wielder.position.Y - (this.wielder.texture.Height * this.wielder.origin.Y) + weaponPos.Y);*/
    }

    private SoundEffect GetAttackSound()
    {
        return Sounds.SwordSwing;
    }
}
