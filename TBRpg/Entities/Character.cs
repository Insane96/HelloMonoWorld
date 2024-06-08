using MonoGame.Aseprite;
using TBRpg.Registries;

namespace TBRpg.Entities;

public class Character : Entity
{
    public Character(SpriteSheet sprite)
    {
        this.SetSprite(Sprites.CreateAnimatedSprite(Sprites.Fighter, "idle"));
    }
}