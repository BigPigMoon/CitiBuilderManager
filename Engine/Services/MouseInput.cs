using Engine.Enums;
using Engine.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Engine.Services;

internal class MouseInput(ICamera2D camera) : IMouseInput
{
    private readonly ICamera2D _camera = camera;
    private MouseState _currentMouseState;
    private MouseState _previousMouseState;

    public Point GetMousePosition()
    {
        var mousePosition = _currentMouseState.Position.ToVector2();
        var cameraTransform = _camera.Transform;
        var inverseCameraTransform = Matrix.Invert(cameraTransform);
        var worldMousePosition = Vector2.Transform(mousePosition, inverseCameraTransform);

        return worldMousePosition.ToPoint();
    }

    public bool IsKeyJustPressed(MouseButtonEnum button)
    {
        switch (button)
        {
            case MouseButtonEnum.Left:
                return _currentMouseState.LeftButton == ButtonState.Pressed && _previousMouseState.LeftButton == ButtonState.Released;
            case MouseButtonEnum.Right:
                return _currentMouseState.RightButton == ButtonState.Pressed && _previousMouseState.RightButton == ButtonState.Released;
            case MouseButtonEnum.Middle:
                return _currentMouseState.MiddleButton == ButtonState.Pressed && _previousMouseState.MiddleButton == ButtonState.Released;
            default:
                return false;
        }
    }

    public bool IsKeyJustReleased(MouseButtonEnum button)
    {
        switch (button)
        {
            case MouseButtonEnum.Left:
                return _currentMouseState.LeftButton == ButtonState.Released && _previousMouseState.LeftButton == ButtonState.Pressed;
            case MouseButtonEnum.Right:
                return _currentMouseState.RightButton == ButtonState.Released && _previousMouseState.RightButton == ButtonState.Pressed;
            case MouseButtonEnum.Middle:
                return _currentMouseState.MiddleButton == ButtonState.Released && _previousMouseState.MiddleButton == ButtonState.Pressed;
            default:
                return false;
        }
    }

    public bool IsKeyPressed(MouseButtonEnum button)
    {
        switch (button)
        {
            case MouseButtonEnum.Left:
                return _currentMouseState.LeftButton == ButtonState.Pressed;
            case MouseButtonEnum.Right:
                return _currentMouseState.RightButton == ButtonState.Pressed;
            case MouseButtonEnum.Middle:
                return _currentMouseState.MiddleButton == ButtonState.Pressed;
            default:
                return false;
        }
    }

    public bool IsKeyReleased(MouseButtonEnum button)
    {
        switch (button)
        {
            case MouseButtonEnum.Left:
                return _currentMouseState.LeftButton == ButtonState.Released;
            case MouseButtonEnum.Right:
                return _currentMouseState.RightButton == ButtonState.Released;
            case MouseButtonEnum.Middle:
                return _currentMouseState.MiddleButton == ButtonState.Released;
            default:
                return false;
        }
    }

    public void Update()
    {
        _previousMouseState = _currentMouseState;
        _currentMouseState = Mouse.GetState();
    }
}

