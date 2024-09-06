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
    private readonly QueryDescription _query = new QueryDescription().WithAll<Child, Visibility>();
    private readonly World _world = world;

    public void Run(in GameTime state)
    {
        _world.Query(in _query, (Entity entity, ref Child child, ref Visibility childVisibility) =>
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
            childVisibility = child.Parent.Get<Visibility>();
        });
    }
}
