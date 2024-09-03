using Engine.Interfaces;
using Microsoft.Xna.Framework.Input;

namespace Engine.Services;

internal class KeyboardInput : IKeyboardInput
{
    private KeyboardState currentKeyboardState;
    private KeyboardState previousKeyboardState;

    public bool IsKeyJustPressed(Keys key)
    {
        return currentKeyboardState.IsKeyDown(key) && previousKeyboardState.IsKeyUp(key);
    }

    public bool IsKeyJustReleased(Keys key)
    {
        return currentKeyboardState.IsKeyUp(key) && previousKeyboardState.IsKeyDown(key);
    }

    public bool IsKeyPressed(Keys key)
    {
        return currentKeyboardState.IsKeyDown(key);
    }

    public bool IsKeyReleased(Keys key)
    {
        return currentKeyboardState.IsKeyUp(key);
    }

    public void Update()
    {
        previousKeyboardState = currentKeyboardState;
        currentKeyboardState = Keyboard.GetState();
    }
}
