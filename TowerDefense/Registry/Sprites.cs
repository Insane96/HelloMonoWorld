using AsepriteDotNet.Aseprite;
using Engine;
using Microsoft.Xna.Framework.Content;
using MonoGame.Aseprite;

namespace TowerDefense.Registry;

public static class Sprites
{
    public static SpriteSheet CrossbowTower { get; private set; }
    public static SpriteSheet DeathTower { get; private set; }
    public static SpriteSheet DeathPool { get; private set; }
    public static SpriteSheet LaserTower { get; private set; }
    public static SpriteSheet LaserUlt { get; private set; }
    public static SpriteSheet Zombie { get; private set; }
    public static SpriteSheet Arrow { get; private set; }

    public static void LoadTextures(ContentManager contentManager)
    {
        CrossbowTower = contentManager.Load<AsepriteFile>("textures/towers/crossbow").CreateSpriteSheet(Graphics.GraphicsDeviceManager.GraphicsDevice, onlyVisibleLayers: true);
        DeathTower = contentManager.Load<AsepriteFile>("textures/towers/death").CreateSpriteSheet(Graphics.GraphicsDeviceManager.GraphicsDevice, onlyVisibleLayers: true);
        DeathPool = contentManager.Load<AsepriteFile>("textures/death_pool").CreateSpriteSheet(Graphics.GraphicsDeviceManager.GraphicsDevice, onlyVisibleLayers: true);
        LaserTower = contentManager.Load<AsepriteFile>("textures/towers/laser").CreateSpriteSheet(Graphics.GraphicsDeviceManager.GraphicsDevice, onlyVisibleLayers: true);
        LaserUlt = contentManager.Load<AsepriteFile>("textures/laser_ult").CreateSpriteSheet(Graphics.GraphicsDeviceManager.GraphicsDevice, onlyVisibleLayers: true);
        Zombie = contentManager.Load<AsepriteFile>("textures/enemies/zombie").CreateSpriteSheet(Graphics.GraphicsDeviceManager.GraphicsDevice, onlyVisibleLayers: true);
        Arrow = contentManager.Load<AsepriteFile>("textures/arrow").CreateSpriteSheet(Graphics.GraphicsDeviceManager.GraphicsDevice, onlyVisibleLayers: true);
    }

    public static AnimatedSprite GetAnimatedSprite(SpriteSheet spriteSheet, string tag)
    {
        return spriteSheet.CreateAnimatedSprite(tag);
    }
}