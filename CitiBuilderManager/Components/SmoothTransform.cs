using Microsoft.Xna.Framework;

namespace CitiBuilderManager.Components;

public class SmoothTransform(Vector2 translation, float rotation, float scale, float depth)
{
    public Vector2 Translation { get; set; } = translation;
    public float Rotation { get; set; } = rotation;
    public float Scale { get; set; } = scale;
    public float Depth { get; set; } = depth;
}
