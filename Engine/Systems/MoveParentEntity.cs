using Arch.Core;
using Arch.Core.Extensions;
using Engine.Attributes;
using Engine.Components;
using Microsoft.Xna.Framework;

namespace Engine.Systems;

[AutoInject]
[OnUpdate]
public class MoveParentEntity(World world) : ISystem
{
    private readonly QueryDescription _query = new QueryDescription().WithAll<Child, Transform2D>();
    private readonly World _world = world;

    public void Run(in GameTime state)
    {
        _world.Query(in _query, (Entity entity, ref Child child, ref Transform2D childTransform) =>
        {
            if (child.Parent == entity)
            {
                return;
            }

            if (!child.Parent.IsAlive())
            {
                _world.Destroy(entity);
                return;
            }

            var parentTransform = child.Parent.Get<Transform2D>();
            var localTransform = entity.AddOrGet(new LocalTransform2D(childTransform.Position, childTransform.Rotation, childTransform.Scale, childTransform.Depth));

            childTransform.Position = parentTransform.Position + localTransform.Position * parentTransform.Scale;
            childTransform.Rotation = parentTransform.Rotation + localTransform.Rotation;
            childTransform.Scale = parentTransform.Scale * localTransform.Scale;
            childTransform.Depth = parentTransform.Depth + localTransform.Depth / 100f;

            // rotate along parent point
            var vecs = childTransform.Position - parentTransform.Position;
            vecs.Rotate(parentTransform.Rotation);
            childTransform.Position = parentTransform.Position + vecs;
        });
    }
}
