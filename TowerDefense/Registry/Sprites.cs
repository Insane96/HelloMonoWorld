using Engine;
using Microsoft.Xna.Framework.Content;
using MonoGame.Aseprite;
using MonoGame.Aseprite.Content.Processors;
using MonoGame.Aseprite.Sprites;

namespace TowerDefense.Registry;

public static class Sprites
{
    public static SpriteSheet CrossbowTower { get; private set; }
    public static SpriteSheet DeathTower { get; private set; }
    public static SpriteSheet DeathPool { get; private set; }
    public static SpriteSheet Zombie { get; private set; }
    public static SpriteSheet Arrow { get; private set; }

    public static void LoadTextures(ContentManager contentManager)
    {
        CrossbowTower = SpriteSheetProcessor.Process(Graphics.GraphicsDeviceManager.GraphicsDevice, contentManager.Load<AsepriteFile>("textures/towers/crossbow"));
        DeathTower = SpriteSheetProcessor.Process(Graphics.GraphicsDeviceManager.GraphicsDevice, contentManager.Load<AsepriteFile>("textures/towers/death"));
        DeathPool = SpriteSheetProcessor.Process(Graphics.GraphicsDeviceManager.GraphicsDevice, contentManager.Load<AsepriteFile>("textures/death_pool"));
        Zombie = SpriteSheetProcessor.Process(Graphics.GraphicsDeviceManager.GraphicsDevice, contentManager.Load<AsepriteFile>("textures/enemies/zombie"));
        Arrow = SpriteSheetProcessor.Process(Graphics.GraphicsDeviceManager.GraphicsDevice, contentManager.Load<AsepriteFile>("textures/arrow"));
    }

    public static AnimatedSprite GetAnimatedSprite(SpriteSheet spriteSheet, string tag)
    {
        return spriteSheet.CreateAnimatedSprite(tag);
    }
}