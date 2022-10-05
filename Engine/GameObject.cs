using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Aseprite.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Transactions;

namespace HelloMonoWorld.Engine;

public abstract class GameObject
{
    //public string Id { get; private set; }

    private Guid guid;

    public Guid Guid
    {
        get { return guid; }
        private set { guid = value; }
    }


    public Vector2 Position { get; set; }
    public Vector2 Origin { get; set; } = new(0.5f, 0.5f);

    public Color Color { get; set; } = Color.White;
    public float Rotation { get; set; }
    public SpriteEffects SpriteEffect { get; private set; } = SpriteEffects.None;

    public bool Enabled { get; set; } = true;
    public bool Visible { get; set; } = true;
    /// <summary>
    /// If true, the GameObject is marked for removal and will be removed at the end of the current update frame
    /// </summary>
    public bool RemovalMark { get; set; }

    public string SpriteName { get; private set; }
    public Texture2D Texture { get; private set; }
    public string AnimatedSpriteName { get; private set; }
    public AnimatedSprite AnimatedSprite { get; private set; }

    private static List<GameObject> GameObjects { get; } = new();
    private static List<GameObject> GameObjectsToInstantiate { get; } = new();

    public GameObject()
    {
        this.Guid = Guid.NewGuid();
    }

    public virtual void Initialize(ContentManager contentManager)
    {
        LoadContent(contentManager);
    }

    public virtual void LoadContent(ContentManager contentManager)
    {
        if (!string.IsNullOrEmpty(SpriteName))
            Texture = contentManager.Load<Texture2D>($"sprites/{SpriteName}");
        else
            this.Visible = false;
    }

    public abstract void Update();

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(
            this.Texture,
            this.Position,
            null,
            this.Color,
            this.Rotation,
            new Vector2(this.Texture.Width * this.Origin.X, this.Texture.Height * this.Origin.Y),
            Vector2.One,
            this.SpriteEffect,
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

    public static void Instantiate(GameObject gameObject)
    {
        GameObjectsToInstantiate.Add(gameObject);
        gameObject.Initialize(MonoEngine.contentManager);
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

    public GameObject SetPosition(Vector2 vector2)
    {
        this.Position = vector2;
        return this;
    }

    public GameObject SetPosition(float x, float y) => this.SetPosition(new Vector2(x, y));

    public GameObject SetSprite(string name)
    {
        this.SpriteName = name;
        return this;
    }

    public GameObject SetAnimatedSprite(string name)
    {
        this.AnimatedSpriteName = name;
        return this;
    }
}
