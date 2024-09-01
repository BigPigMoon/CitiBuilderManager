using Microsoft.Xna.Framework;

namespace Engine.Components;

public struct Transform2D
{
    public Vector3 Translation { get; set; }
    public float Rotation { get; set; }
    public float Scale { get; set; }
}
