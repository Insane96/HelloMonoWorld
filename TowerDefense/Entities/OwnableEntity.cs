using MonoGame.Aseprite.Sprites;
using TowerDefense.Entities.Towers;

namespace TowerDefense.Entities;

public class OwnableEntity : Entity
{
    public Tower Owner { get; protected set; }
    
    public OwnableEntity(AnimatedSprite sprite, Tower owner) : base(sprite)
    {
        this.Owner = owner;
    }
}