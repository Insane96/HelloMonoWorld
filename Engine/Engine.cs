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

        public static void Init(ContentManager content)
        {
            contentManager = content;
        }

        public static void Instantiate(GameObject gameObject)
        {
            gameObjects.Add(gameObject);
            gameObject.Initialize(contentManager);
        }

        public static void UpdateGameObjects()
        {
            foreach (GameObject gameObject in gameObjects)
            {
                if (gameObject.Enabled)
                    gameObject.Update();
            }

            gameObjects.RemoveAll(g => g.RemovalMark);
        }

        public static void DrawGameObjects(SpriteBatch spriteBatch)
        {
            foreach (GameObject gameObject in gameObjects)
            {
                if (gameObject.Visible)
                    gameObject.Draw(spriteBatch);
            }
        }
    }
}
