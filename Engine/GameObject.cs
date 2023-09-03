using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Aseprite.Documents;
using MonoGame.Aseprite.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HelloMonoWorld.Engine;

public abstract class GameObject
{
    public Guid Guid { get; }

    public Vector2 origin;
    public Vector2 Origin
    {
        get => this.origin;
        set
        {
            this.origin = value;
            this.Sprite.Origin = new Vector2(this.Sprite.Width * origin.X, this.Sprite.Height * origin.Y);
        }
    }

    public bool Enabled { get; set; } = true;
    public bool Visible { get; set; } = true;
    /// <summary>
    /// If true, the GameObject is marked for removal and will be removed at the end of the current update frame
    /// </summary>
    public bool RemovalMark { get; set; }

    public AnimatedSprite Sprite { get; private set; }

    private static List<GameObject> GameObjects { get; } = new();
    private static List<GameObject> GameObjectsToInstantiate { get; } = new();

    public GameObject()
    {
        this.Guid = Guid.NewGuid();
        this.Sprite = new AnimatedSprite(Utils.OneByOneTexture);
    }

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
        this.Sprite.Render(spriteBatch);
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
        gameObject.Initialize(MonoEngine.ContentManager);
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

    public void SetSprite(AsepriteDocument aseprite)
    {
        this.Sprite = new AnimatedSprite(aseprite);
        this.Origin = Origins.Center;
    }

    public void SetPosition(Vector2 vector2) => this.Sprite.Position = vector2;

    public void SetPosition(float x, float y) => this.SetPosition(new Vector2(x, y));
    public void Travel(Vector2 value) => this.Sprite.Position += value;

    public void SetX(float x) => this.Sprite.Position = new(x, this.Sprite.Y);
    public void SetY(float y) => this.Sprite.Position = new(this.Sprite.X, y);

    public Vector2 GetPosition() => this.Sprite.Position;
    public float GetX() => this.Sprite.X;
    public float GetY() => this.Sprite.Y;

    public int GetWidth() => this.Sprite.Width;
    public int GetHeight() => this.Sprite.Height;

    public void SetColor(Color color) => this.Sprite.Color = color;

    public void SetRotation(float rot) => this.Sprite.Rotation = rot;

    public void SetScale(Vector2 scale) => this.Sprite.Scale = scale;

    public void SetSpriteEffect(SpriteEffects spriteEffect) => this.Sprite.SpriteEffect = spriteEffect;
}
