using Engine.Interfaces;
using Microsoft.Xna.Framework;

namespace Engine.Services;

internal class Camera2D(IWindowManager windowManager) : ICamera2D
{
    public Matrix Transform { get; private set; }
    public Vector2 Position { get; set; }
    public float Zoom { get; set; } = 1f;

    private readonly IWindowManager _windowsManager = windowManager;

    public void Update()
    {
        Transform = Matrix.CreateTranslation(new Vector3(-Position, 0)) *
                    Matrix.CreateScale(Zoom) *
                    Matrix.CreateTranslation(new Vector3(_windowsManager.ScreenWidth * 0.5f, _windowsManager.ScreenHeight * 0.5f, 0));
    }
}
