using Microsoft.Xna.Framework.Input;

namespace Engine.Interfaces;

public interface IKeyboardInput
{
    void Update();
    bool IsKeyJustPressed(Keys key);
    bool IsKeyJustReleased(Keys key);
    bool IsKeyPressed(Keys key);
    bool IsKeyReleased(Keys key);
}