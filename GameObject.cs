using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace HelloMonoWorld;

internal abstract class GameObject
{
	public string Id { get; private set; }

    public Vector2 position;
    public Vector2 origin = new Vector2(0.5f, 0.5f);

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

    public virtual void LoadContent(ContentManager contentManager)
    {
        this.texture = contentManager.Load<Texture2D>(this.spriteName);
    }

    public abstract void Update();

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(
            this.texture,
            this.position,
            null,
            Color.Black,
            0f,
            new Vector2(this.texture.Width * this.origin.X, this.texture.Height * this.origin.Y),
            Vector2.One,
            SpriteEffects.None,
            0f
        );
    }
}
