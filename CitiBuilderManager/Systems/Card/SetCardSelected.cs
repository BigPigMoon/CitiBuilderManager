using Arch.Core;
using CitiBuilderManager.Components;
using CitiBuilderManager.Interfaces;
using Engine.Attributes;
using Engine.Components;
using Engine.Interfaces;
using Engine.Systems;
using Microsoft.Xna.Framework;

namespace CitiBuilderManager.Systems;

[AutoInject]
[OnUpdate]
public class SetCardSelected(World world, IMouseInput mouseInput, ICardManager cardManager) : ISystem
{
    public readonly QueryDescription _cardQuery = new QueryDescription().WithAll<CardComponent, BoxColliderComponent, Transform2D>();
    private readonly World _world = world;
    private readonly IMouseInput _mouseInput = mouseInput;
    private readonly ICardManager _cardManager = cardManager;

    public void Run(in GameTime state)
    {
        _cardManager.SelectedCard = GetHoveredCard();
    }

    private Entity? GetHoveredCard()
    {
        Entity? hoveredCard = null;
        var maxZ = float.NegativeInfinity;

        _world.Query(in _cardQuery, (Entity entity, ref BoxColliderComponent collider, ref Transform2D transform) =>
        {
            var mousePos = _mouseInput.GetMousePosition();

            if (collider.ContainsPoint(mousePos) && transform.Depth > maxZ)
            {
                maxZ = transform.Depth;
                hoveredCard = entity;
            }
        });

        return hoveredCard;
    }
}
