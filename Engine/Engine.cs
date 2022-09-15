using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloMonoWorld.Engine
{
    public class Engine
    {
        static ContentManager contentManager;

        public static List<GameObject> gameObjects = new();
        private static List<GameObject> newGameObjects = new();

        public static void Init(ContentManager content)
        {
            contentManager = content;
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

        public static void DrawText(SpriteBatch spriteBatch, SpriteFont font, string text, Vector2 position, Color color, Color? shadowColor = null)
        {
            if (shadowColor.HasValue)
                spriteBatch.DrawString(font, text, position, shadowColor.Value);
            spriteBatch.DrawString(font, text, position, color);
        }
    }
}
