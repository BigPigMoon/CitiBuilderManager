using Microsoft.Xna.Framework;

namespace Engine.Interfaces;

public interface ICamera2D
{
    Matrix Transform { get; }
    Vector2 Position { get; set; }
    float Zoom { get; set; }

    public void Update();
}
