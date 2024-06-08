using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TBRpg.Options;

namespace TBRpg;

public class RpgGame() : EngineGame(1280, 720)
{
    public SpriteFont BaseFont { get; private set; } = null!;

    protected override void Initialize()
    {
        BaseFont = Content.Load<SpriteFont>("fonts/BaseFont");
        base.Initialize();
    }

    protected override void LoadContent()
    {
        base.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        if (Input.IsKeyPressed(Keys.PageUp))
            Graphic.IncreaseScale();
        else if (Input.IsKeyPressed(Keys.PageDown))
            Graphic.DecreaseScale();
        AddText(BaseFont, $"Hello World {Graphic.TextScale}", new Vector2(10, 10), Color.BlanchedAlmond, Origins.TopLeft);
    }

    protected override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);
    }

    public override void AddText(SpriteFont font, string text, Vector2 position, Color color, Vector2 origin, Vector2 scale, float rotation = 0, Color? shadowColor = null)
    {
        base.AddText(font, text, position, color, origin, scale * Graphic.TextScale, rotation, shadowColor);
    }
}