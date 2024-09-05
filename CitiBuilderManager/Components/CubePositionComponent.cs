using Microsoft.Xna.Framework;

namespace CitiBuilderManager.Components;

internal class CubePositionComponent(Vector2 position)
{
    public Vector2 Position { get; set; } = position;
}
