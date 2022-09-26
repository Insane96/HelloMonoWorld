using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Transactions;

namespace HelloMonoWorld.Engine;

public abstract class GameObject
{
    public string Id { get; private set; }

    public Vector2 Position { get; set; }
    public Vector2 Origin { get; set; } = new(0.5f, 0.5f);

    public Color Color { get; set; } = Color.White;
    public SpriteEffects SpriteEffect { get; private set; } = SpriteEffects.None;

    public bool Enabled { get; set; } = true;
    public bool Visible { get; set; } = true;
    /// <summary>
    /// If true, the GameObject is marked for removal and will be removed at the end of the current update frame
    /// </summary>
    public bool RemovalMark { get; set; }

    public string SpriteName { get; private set; }
    public Texture2D Texture { get; private set; }

    public GameObject(string id) : this(id, "", Vector2.Zero) { Visible = false; }

    public GameObject(string id, string spriteName) : this(id, spriteName, Vector2.Zero) { }

    public GameObject(string id, string spriteName, Vector2 position)
    {
        Id = id;
        this.SpriteName = spriteName;
        this.Position = position;
    }

    public virtual void Initialize(ContentManager contentManager)
    {
        LoadContent(contentManager);
    }

    public virtual void LoadContent(ContentManager contentManager)
    {
        if (!string.IsNullOrEmpty(SpriteName))
            Texture = contentManager.Load<Texture2D>($"sprites/{SpriteName}");
    }

    public abstract void Update();

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(
            Texture,
            Position,
            null,
            Color,
            0f,
            new Vector2(Texture.Width * Origin.X, Texture.Height * Origin.Y),
            Vector2.One,
            SpriteEffect,
            0f
        );
    }

    public void Enable() => Enabled = true;
    public void Disable() => Enabled = false;

    public void Show() => Visible = true;
    public void Hide() => Visible = false;
    public void MarkForRemoval() => RemovalMark = true;

    public void HideAndDisable()
    {
        Disable();
        Hide();
    }
}
