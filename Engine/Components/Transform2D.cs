using Microsoft.Xna.Framework;

namespace Engine.Components;

public class Transform2D(Vector2 translation, float rotation, float scale, float depth)
{
    public Vector2 Position { get; set; } = translation;
    public float Rotation { get; set; } = rotation;
    public float Scale { get; set; } = scale;
    public float Depth { get; set; } = depth;
}
