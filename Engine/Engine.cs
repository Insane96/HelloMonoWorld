using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HelloMonoWorld.Engine
{
    public class Engine
    {
        static ContentManager contentManager;

        public static List<GameObject> gameObjects = new();
        private static List<GameObject> newGameObjects = new();

        public static List<UIText> stringsToDraw = new();

        public static void Init(ContentManager content)
        {
            contentManager = content;
            UIText.Init(contentManager);
        }

        public static void Instantiate(GameObject gameObject)
        {
            newGameObjects.Add(gameObject);
            gameObject.Initialize(contentManager);
        }

        public static void UpdateGameObjects(GameTime gameTime)
        {
            Time.UpdateDeltaTime(gameTime);

            foreach (GameObject gameObject in gameObjects)
            {
                if (gameObject.Enabled)
                    gameObject.Update();
            }

            gameObjects.RemoveAll(g => g.RemovalMark);
            if (newGameObjects.Count > 0)
            {
                gameObjects.AddRange(newGameObjects);
                newGameObjects.Clear();
            }
        }

        public static void DrawGameObjects(SpriteBatch spriteBatch)
        {
            foreach (GameObject gameObject in gameObjects)
            {
                if (gameObject.Visible)
                    gameObject.Draw(spriteBatch);
            }
        }

        public static void DrawStrings(SpriteBatch spriteBatch)
        {
            foreach (UIText text in stringsToDraw)
            {
                Vector2 textSize = text.SpriteFont.MeasureString(text.Text);
                Vector2 position = text.Position.Multiply(Graphics.ScaledRatio).Sum(-textSize.X * text.Origin.X, -textSize.Y * text.Origin.Y);
                if (text.ShadowColor.HasValue)
                    spriteBatch.DrawString(text.SpriteFont, text.Text, position.Sum(1, 1), text.ShadowColor.Value);
                spriteBatch.DrawString(text.SpriteFont, text.Text, position, text.Color);
            }
            stringsToDraw.Clear();
        }

        public static void DrawText(SpriteFont font, string text, Vector2 position, Color color, Vector2 origin, Color? shadowColor = null)
        {
            stringsToDraw.Add(new UIText()
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
