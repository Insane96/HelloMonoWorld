using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Transactions;

namespace HelloMonoWorld.Engine;

public abstract class GameObject
{
    public string Id { get; private set; }

    public Vector2 position;
    public Vector2 origin = new(0.5f, 0.5f);

    public Color color = Color.White;
    public SpriteEffects spriteEffect = SpriteEffects.None;

    public bool Enabled { get; set; } = true;
    public bool Visible { get; set; } = true;
    public bool RemovalMark { get; set; }

    public string spriteName { get; private set; }
    public Texture2D texture { get; private set; }

    public GameObject(string id) : this(id, "", Vector2.Zero) { Visible = false; }

    public GameObject(string id, string spriteName) : this(id, spriteName, Vector2.Zero) { }

    public GameObject(string id, string spriteName, Vector2 position)
    {
        Id = id;
        this.spriteName = spriteName;
        this.position = position;
    }

    public virtual void Initialize(ContentManager contentManager)
    {
        LoadContent(contentManager);
    }

    public virtual void LoadContent(ContentManager contentManager)
    {
        if (!string.IsNullOrEmpty(spriteName))
            texture = contentManager.Load<Texture2D>($"sprites/{spriteName}");
    }

    public abstract void Update();

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(
            texture,
            position,
            null,
            color,
            0f,
            new Vector2(texture.Width * origin.X, texture.Height * origin.Y),
            Vector2.One,
            spriteEffect,
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
