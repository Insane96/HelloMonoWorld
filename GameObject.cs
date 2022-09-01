using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace HelloMonoWorld;

internal abstract class GameObject
{
	public string Id { get; private set; }

    public Vector2 position;

    public bool Enabled { get; set; } = true;
    public bool Visible { get; set; } = true;

    public string spriteName { get; private set; }
    public Texture2D texture { get; private set; }

    public GameObject(string id, string spriteName) : this(id, spriteName, Vector2.Zero) { }

    public GameObject(string id, string spriteName, Vector2 position)
    {
        this.Id = id;
        this.spriteName = spriteName;
        this.position = position;
    }

    public abstract void Initialize();

    public void LoadContent(ContentManager contentManager)
    {
        this.texture = contentManager.Load<Texture2D>(this.spriteName);
    }

    public abstract void Update();

    public abstract void Draw(SpriteBatch spriteBatch);
}
