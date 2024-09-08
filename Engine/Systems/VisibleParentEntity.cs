using Arch.Core;
using Arch.Core.Extensions;
using Engine.Attributes;
using Engine.Components;
using Microsoft.Xna.Framework;

namespace Engine.Systems;

[AutoInject]
[OnUpdate]
public class VisibleParentEntity(World world) : ISystem
{
    private readonly QueryDescription _query = new QueryDescription().WithAll<Child, VisibilityComponent>();
    private readonly World _world = world;

    public void Run(in GameTime state)
    {
        _world.Query(in _query, (Entity entity, ref Child child, ref VisibilityComponent childVisibility) =>
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

            if (!child.Parent.Has<VisibilityComponent>())
            {
                return;
            }

            childVisibility = child.Parent.Get<VisibilityComponent>();
        });
    }
}
