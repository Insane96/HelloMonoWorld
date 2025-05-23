using System.IO;
using AsepriteDotNet.IO;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Aseprite;

namespace TowerDefense.Registry;

public static class Sprites
{
    public static readonly string TexturesPath = "assets/textures/";
    
    public static SpriteSheet CrossbowTower { get; private set; }
    public static SpriteSheet DeathTower { get; private set; }
    public static SpriteSheet DeathPool { get; private set; }
    public static SpriteSheet LaserTower { get; private set; }
    public static SpriteSheet LaserAbility { get; private set; }
    public static SpriteSheet Zombie { get; private set; }
    public static SpriteSheet Arrow { get; private set; }
    
    private static GraphicsDevice _graphicsDevice;

    public static void LoadTextures(GraphicsDevice graphicsDevice)
    {
        _graphicsDevice = graphicsDevice;
        CrossbowTower = LoadSpriteSheet("towers/crossbow");
        DeathTower = LoadSpriteSheet("towers/death");
        DeathPool = LoadSpriteSheet("death_pool");
        LaserTower = LoadSpriteSheet("towers/laser");
        LaserAbility = LoadSpriteSheet("laser_ability");
        Zombie = LoadSpriteSheet("enemies/zombie");
        Arrow = LoadSpriteSheet("arrow");
    }

    public static AnimatedSprite GetAnimatedSprite(SpriteSheet spriteSheet, string tag)
    {
        return spriteSheet.CreateAnimatedSprite(tag);
    }
    
    private static SpriteSheet LoadSpriteSheet(string relativePath)
    {
        string fullPath = Path.Combine(TexturesPath, relativePath + ".aseprite");

        if (!File.Exists(fullPath))
            throw new FileNotFoundException($"Aseprite file not found: {fullPath}");

        return AsepriteFileLoader.FromFile(fullPath)
            .CreateSpriteSheet(_graphicsDevice, onlyVisibleLayers: true);
    }
}