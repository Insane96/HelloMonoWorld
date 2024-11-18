using AsepriteDotNet.IO;
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
    public static SpriteSheet LaserAbility { get; private set; }
    public static SpriteSheet Zombie { get; private set; }
    public static SpriteSheet Arrow { get; private set; }

    public static void LoadTextures(ContentManager contentManager)
    {
        CrossbowTower = CreateSpriteSheet("towers/crossbow");
        DeathTower = CreateSpriteSheet("towers/death");
        DeathPool = CreateSpriteSheet("death_pool");
        LaserTower = CreateSpriteSheet("towers/laser");
        LaserAbility = CreateSpriteSheet("laser_ability");
        Zombie = CreateSpriteSheet("enemies/zombie");
        Arrow = CreateSpriteSheet("arrow");
    }

    public static AnimatedSprite GetAnimatedSprite(SpriteSheet spriteSheet, string tag)
    {
        return spriteSheet.CreateAnimatedSprite(tag);
    }
    
    private static SpriteSheet CreateSpriteSheet(string path)
    {
        return AsepriteFileLoader.FromFile("assets/textures/" + path + ".aseprite").CreateSpriteSheet(Graphics.GraphicsDeviceManager.GraphicsDevice, onlyVisibleLayers: true);
    }
}