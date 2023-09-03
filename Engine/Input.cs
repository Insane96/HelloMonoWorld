using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace HelloMonoWorld.Engine;

public class Input
{
    private static KeyboardState _oldKeyboardState;
    public static KeyboardState KeyboardState { get; private set; }
    private static GamePadState _oldGamePadState;
    public static GamePadState GamePadState { get; private set; }

    public static void Update()
    {
        _oldKeyboardState = KeyboardState;
        KeyboardState = Keyboard.GetState();
        _oldGamePadState = GamePadState;
        GamePadState = GamePad.GetState(PlayerIndex.One);
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
            Debug.WriteLine(KeyboardState.IsKeyDown(key) + " " + _oldKeyboardState.IsKeyDown(key));
        return KeyboardState.IsKeyDown(key) && !_oldKeyboardState.IsKeyDown(key);
    }
}