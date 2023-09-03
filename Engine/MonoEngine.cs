using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace HelloMonoWorld.Engine
{
    /// <summary>
    /// The core of the game, updates and draws everything
    /// </summary>
    public static class MonoEngine
    {
        internal static ContentManager ContentManager;

        private static List<UiText> StringsToDraw { get; } = new();

        public static void Init(Microsoft.Xna.Framework.Game game, int width, int height)
        {
            ContentManager = game.Content;
            Graphics.Init(game, width, height);
            UiText.Init(ContentManager);

            Utils.Init();
        }

        public static void UpdateGameObjects(GameTime gameTime)
        {
            Time.UpdateDeltaTime(gameTime);

            GameObject.UpdateGameObjects();
        }

        public static void DrawGameObjects(SpriteBatch spriteBatch)
        {
            GameObject.DrawGameObjects(spriteBatch);
        }

        public static void DrawStrings(SpriteBatch spriteBatch)
        {
            foreach (UiText text in StringsToDraw)
            {
                Vector2 textSize = text.SpriteFont.MeasureString(text.Text);
                Vector2 position = text.Position.Multiply(Graphics.ScaledRatio).Sum(-textSize.X * text.Origin.X, -textSize.Y * text.Origin.Y);
                if (text.ShadowColor.HasValue)
                    spriteBatch.DrawString(text.SpriteFont, text.Text, position.Sum(1, 1), text.ShadowColor.Value);
                spriteBatch.DrawString(text.SpriteFont, text.Text, position, text.Color);
            }
            StringsToDraw.Clear();
        }

        public static void DrawText(SpriteFont font, string text, Vector2 position, Color color, Vector2 origin, Color? shadowColor = null)
        {
            StringsToDraw.Add(new UiText()
            {
                Text = text,
                SpriteFont = font,
                Position = position,
                Color = color,
                Origin = origin,
                ShadowColor = shadowColor
            });
        }
    }
}
