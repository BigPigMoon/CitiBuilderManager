using Engine.Enums;
using Microsoft.Xna.Framework;

namespace Engine.Interfaces;

public interface IMouseInput
{
    Point GetMousePosition();
    void Update();
    bool IsKeyJustPressed(MouseButtonEnum button);
    bool IsKeyJustReleased(MouseButtonEnum button);
    bool IsKeyPressed(MouseButtonEnum button);
    bool IsKeyReleased(MouseButtonEnum button);
}
