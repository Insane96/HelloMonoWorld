using AsepriteDotNet.Aseprite;
using Engine;
using Microsoft.Xna.Framework.Content;
using MonoGame.Aseprite;

namespace TBRpg.Registries;

#nullable disable
public static class Sprites
{
    public static SpriteSheet Fighter { get; private set; }

    public static void LoadSpriteSheets(ContentManager contentManager)
    {
        Fighter = contentManager.Load<AsepriteFile>("entities/characters/fighter").CreateSpriteSheet(Graphics.GraphicsDeviceManager.GraphicsDevice);
    }

    public static AnimatedSprite CreateAnimatedSprite(SpriteSheet spriteSheet, string tagName = "")
    {
        return spriteSheet.CreateAnimatedSprite(tagName);
    }
}