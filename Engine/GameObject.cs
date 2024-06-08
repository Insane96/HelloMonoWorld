using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Aseprite;

namespace Engine;

public abstract class GameObject
{
    internal static Game Game = null!;
    public Guid Guid { get; } = Guid.NewGuid();

    public Vector2 Position { get; set; }

    private Vector2 _origin;
    /// <summary>
    /// Gets or Sets the x- and y-coordinate (in percentage 0~1) of origin to apply when rendering the <see cref="T:MonoGame.Aseprite.Sprites.Sprite" />.
    /// </summary>
    public Vector2 Origin
    {
        get => this._origin;
        set
        {
            this._origin = value;
            this.Sprite.Origin = new Vector2(this.Sprite.Width * _origin.X, this.Sprite.Height * _origin.Y);
        }
    }

    public bool Enabled { get; set; } = true;
    public bool Visible { get; set; } = true;
    /// <summary>
    /// If true, the GameObject is marked for removal and will be removed at the end of the current update frame
    /// </summary>
    public bool RemovalMark { get; set; }

    public AnimatedSprite? Sprite { get; private set; }

    private static List<GameObject> GameObjects { get; } = new();
    private static List<GameObject> GameObjectsToInstantiate { get; } = new();

    public virtual void Initialize(ContentManager contentManager)
    {
        LoadContent(contentManager);
    }

    public virtual void LoadContent(ContentManager contentManager)
    {
        if (this.Sprite == null)
            this.Visible = false;
    }

    public virtual void Update()
    {
        this.Sprite.Update((float)Time.DeltaTime);
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        this.Sprite?.Draw(spriteBatch, this.Position);
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

    public static void Instantiate(GameObject gameObject)
    {
        GameObjectsToInstantiate.Add(gameObject);
        gameObject.Initialize(Game.Content);
    }

    internal static void UpdateGameObjects()
    {
        foreach (GameObject gameObject in GetUpdatableGameObjects())
        {
            gameObject.Update();
        }
        RemoveMarkedForRemoval();
        AddInstantiated();
    }

    internal static void DrawGameObjects(SpriteBatch spriteBatch)
    {
        foreach (GameObject gameObject in GetDrawableGameObjects())
        {
            gameObject.Draw(spriteBatch);
        }
    }

    public static IEnumerable<GameObject> GetUpdatableGameObjects()
    {
        return GameObjects.Where(g => g.Enabled);
    }

    internal static void RemoveMarkedForRemoval()
    {
        GameObjects.RemoveAll(g => g.RemovalMark);
    }

    internal static void AddInstantiated()
    {
        if (GameObjectsToInstantiate.Count > 0)
        {
            GameObjects.AddRange(GameObjectsToInstantiate);
            GameObjectsToInstantiate.Clear();
        }
    }

    internal static IEnumerable<GameObject> GetDrawableGameObjects()
    {
        return GameObjects.Where(g => g.Visible);
    }
    
    public void SetSprite(AnimatedSprite sprite)
    {
        this.Sprite = sprite;
        this.Origin = Origins.Center;
    }

    public void SetPosition(Vector2 vector2) => this.Position = vector2;

    public void Move(Vector2 value) => this.Position += value;

    public void SetX(float x) => this.Position = new Vector2(x, this.Position.Y);
    public void SetY(float y) => this.Position = new Vector2(this.Position.X, y);

    public float GetX() => this.Position.X;
    public float GetY() => this.Position.Y;

    public int GetWidth() => (int)(this.Sprite.Width * this.Sprite.ScaleX);
    public int GetHeight() => (int)(this.Sprite.Height * this.Sprite.ScaleY);

    public void SetColor(Color color) => this.Sprite.Color = color;

    public void SetRotation(float rot) => this.Sprite.Rotation = rot;

    public void SetScale(Vector2 scale) => this.Sprite.Scale = scale;

    public void SetSpriteEffect(SpriteEffects spriteEffect) => this.Sprite.SpriteEffects = spriteEffect;
}
