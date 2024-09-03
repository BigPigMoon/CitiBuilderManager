using Engine.Interfaces;
using Microsoft.Xna.Framework.Graphics;

namespace Engine.Services;

internal class WindowManager : IWindowManager
{
    private readonly GraphicsDevice _graphicsDevice;

    public WindowManager(GraphicsDevice device)
    {
        _graphicsDevice = device;
    }

    public int ScreenWidth => _graphicsDevice.Viewport.Width;
    public int ScreenHeight => _graphicsDevice.Viewport.Height;
}
