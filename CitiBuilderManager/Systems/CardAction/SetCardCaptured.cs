using Arch.Core.Extensions;
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
public class SetCardCaptured(IMouseInput mouseInput, ICardManager cardManager, IBuildingManager buildingManager, IMapManager map) : ISystem
{
    private readonly IMouseInput _mouseInput = mouseInput;
    private readonly ICardManager _cardManager = cardManager;
    private readonly IBuildingManager _buildingManager = buildingManager;
    private readonly IMapManager _map = map;

    public void Run(in GameTime state)
    {
        if (_cardManager.SelectedCard == null)
            return;

        if (_mouseInput.IsKeyPressed(Engine.Enums.MouseButtonEnum.Left))
        {
            _cardManager.CapturedCard = _cardManager.SelectedCard;
            _cardManager.CapturedCard.Value.Remove<SmoothTransformComponent>();
        }
        else
        {
            if (_cardManager.CapturedCard != null)
            {
                var cardVisible = _cardManager.CapturedCard.Value.Get<VisibilityComponent>().Visibility == Visibility.Visible;
                if (!cardVisible && _buildingManager.CapturedBuilding != null && !_map.IsBooked(_buildingManager.CapturedBuilding.Value))
                {
                    _map.AddBuilding(_buildingManager.CapturedBuilding.Value);
                    _buildingManager.DespawnCapturedBuilding();
                    _cardManager.DespawnCard(_cardManager.CapturedCard.Value);
                }
                else
                {
                    var transform = _cardManager.CapturedCard.Value.Get<Transform2D>();
                    _cardManager.CapturedCard.Value.Add(new SmoothTransformComponent(transform.Position, transform.Rotation, transform.Scale, transform.Depth));
                    _cardManager.CapturedCard.Value.Set(new VisibilityComponent(Visibility.Visible));
                }
            }
            _cardManager.CapturedCard = null;
        }
    }
}
