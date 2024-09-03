using Arch.Core;
using CitiBuilderManager.Components;
using CitiBuilderManager.Enums;
using Engine.Attributes;
using Engine.Interfaces;
using Engine.Systems;
using Microsoft.Xna.Framework;

namespace CitiBuilderManager.Systems;

[AutoInject]
[OnUpdate]
internal class CardStateController(World world, IMouseInput mouse) : ISystem
{
    private readonly QueryDescription _query = new QueryDescription().WithAll<Card, BoxCollider>();
    private readonly World _world = world;
    private readonly IMouseInput _mouse = mouse;

    public void Run(in GameTime state)
    {
        _world.Query(in _query, (ref Card card, ref BoxCollider collider) =>
        {

        });
    }
}
