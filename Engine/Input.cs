using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Engine;

public class Input
{
    private static KeyboardState _oldKeyboardState;
    public static KeyboardState KeyboardState { get; private set; }
    private static GamePadState _oldGamePadState;
    public static GamePadState GamePadState { get; private set; }

    private static MouseState _oldMouseState;
    public static MouseState MouseState { get; private set; }

    public static void Update()
    {
        _oldKeyboardState = KeyboardState;
        KeyboardState = Keyboard.GetState();
        _oldGamePadState = GamePadState;
        GamePadState = GamePad.GetState(PlayerIndex.One);
        _oldMouseState = MouseState;
        MouseState = Mouse.GetState();
    }

    /// <summary>
    /// Returns true if the key is down
    /// </summary>
    public static bool IsKeyDown(Keys key)
    {
        return KeyboardState.IsKeyDown(key);
    }

    /// <summary>
    /// Returns true if the key has been pressed and wasn't previously pressed
    /// </summary>
    public static bool IsKeyPressed(Keys key)
    {
        if (KeyboardState.IsKeyDown(key))
            Debug.WriteLine($"{KeyboardState.IsKeyDown(key)} {_oldKeyboardState.IsKeyDown(key)}");
        return KeyboardState.IsKeyDown(key) && !_oldKeyboardState.IsKeyDown(key);
    }

    /// <summary>
    /// Returns true if left click has been pressed and wasn't previously pressed
    /// </summary>
    public static bool IsLeftClickPressed()
    {
        /*if (MouseState.LeftButton == ButtonState.Pressed)
            Debug.WriteLine($"{MouseState.LeftButton} {_oldMouseState.LeftButton}");*/
        return MouseState.LeftButton == ButtonState.Pressed && _oldMouseState.LeftButton != ButtonState.Pressed && IsMouseInBounds();
    }

    /// <summary>
    /// Returns true if right click has been pressed and wasn't previously pressed
    /// </summary>
    public static bool IsRightClickPressed()
    {
        /*if (MouseState.RightButton == ButtonState.Pressed)
            Debug.WriteLine($"{MouseState.RightButton} {_oldMouseState.RightButton}");*/
        return MouseState.RightButton == ButtonState.Pressed && _oldMouseState.RightButton != ButtonState.Pressed && IsMouseInBounds();
    }

    public static bool IsMouseInBounds()
    {
        return MouseState.X >= 0 && MouseState.X <= Graphics.Width && MouseState.Y >= 0 && MouseState.Y <= Graphics.Height;
    }
}