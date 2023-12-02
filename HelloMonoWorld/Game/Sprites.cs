using Microsoft.Xna.Framework.Content;

namespace HelloMonoWorld.Game
{
    public static class Sprites
    {
        public static AsepriteDocument StickmanAnimatedAseprite;
        public static AsepriteDocument MagicBulletTexture;
        public static AsepriteDocument EnergyBallTexture;

        public static void LoadTextures(ContentManager contentManager)
        {
            StickmanAnimatedAseprite = contentManager.Load<AsepriteDocument>("sprites/stickman_anim");
            MagicBulletTexture = contentManager.Load<AsepriteDocument>("sprites/magic_bullet");
            EnergyBallTexture = contentManager.Load<AsepriteDocument>("sprites/energy_ball");
        }
    }
}
