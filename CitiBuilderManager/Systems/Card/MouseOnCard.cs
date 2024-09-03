using Arch.Core;
using CitiBuilderManager.Components;
using Engine.Attributes;
using Engine.Interfaces;
using Engine.Systems;
using Microsoft.Extensions.Logging;
using Microsoft.Xna.Framework;

namespace CitiBuilderManager.Systems;

[AutoInject]
[OnUpdate]
internal class MouseOnCard(World world, IMouseInput mouseInput, ILogger<MouseOnCard> logger) : ISystem
{
    private readonly QueryDescription _queryDescription = new QueryDescription().WithAll<BoxCollider, Card>();
    private readonly World _world = world;
    private readonly IMouseInput _mouseInput = mouseInput;
    private readonly ILogger<MouseOnCard> _logger = logger;

    public void Run(in GameTime state)
    {
        _world.Query(in _queryDescription, (Entity entity, ref BoxCollider collider, ref Card order) =>
        {
            var point = _mouseInput.GetMousePosition();
            var contains = collider.ContainsPoint(point);

            if (contains)
            {
                _logger.LogInformation("Mouse on card {Entity}", entity);
            }
        });
    }
}
