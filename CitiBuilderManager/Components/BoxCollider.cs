using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace CitiBuilderManager.Components;

public class BoxColliderComponent(Vector2 center, float rotation, Vector2 halfSize)
{
    public Vector2 Center { get; set; } = center;
    public float Rotation { get; set; } = rotation;
    public Vector2 HalfSize { get; set; } = halfSize;

    public List<Vector2> GetPoints()
    {
        return
        [
            Vector2.Add(Center, Vector2.Rotate(Vector2.Multiply(HalfSize, new Vector2(-1.0f, -1.0f)), Rotation)),
            Vector2.Add(Center, Vector2.Rotate(Vector2.Multiply(HalfSize, new Vector2(1.0f, -1.0f)), Rotation)),
            Vector2.Add(Center, Vector2.Rotate(Vector2.Multiply(HalfSize, new Vector2(1.0f, 1.0f)), Rotation)),
            Vector2.Add(Center, Vector2.Rotate(Vector2.Multiply(HalfSize, new Vector2(-1.0f, 1.0f)), Rotation))
        ];
    }

    public bool ContainsPoint(Vector2 point)
    {
        return ContainsPointBase(point.X, point.Y);
    }

    public bool ContainsPoint(Point point)
    {
        return ContainsPointBase(point.X, point.Y);
    }

    private bool ContainsPointBase(float x, float y)
    {
        var count = 0;
        var vertices = GetPoints();

        var n = vertices.Count;

        for (int i = 0; i < n; i++)
        {
            var p1 = vertices[i];
            var p2 = vertices[(i + 1) % n];

            if ((p1.Y > y) != (p2.Y > y)
                && (x < (p2.X - p1.X) * (y - p1.Y) / (p2.Y - p1.Y) + p1.X))
            {
                count++;
            }
        }

        return count % 2 != 0;
    }
}
