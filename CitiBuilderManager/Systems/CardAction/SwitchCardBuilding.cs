using Arch.Core.Extensions;
using CitiBuilderManager.Components;
using CitiBuilderManager.Constants;
using CitiBuilderManager.Interfaces;
using Engine.Attributes;
using Engine.Components;
using Engine.Interfaces;
using Engine.Systems;
using Microsoft.Extensions.Logging;
using Microsoft.Xna.Framework;

namespace CitiBuilderManager.Systems.Card;

[AutoInject]
[OnUpdate]
public class SwitchCardBuilding(IWindowManager windowManager, ICamera2D camera, IMouseInput mouseInput, ICardManager cardManager, IBuildingManager buildingManager) : ISystem
{
    private readonly IWindowManager _windowManager = windowManager;
    private readonly ICamera2D _camera = camera;
    private readonly IMouseInput _mouseInput = mouseInput;
    private readonly ICardManager _cardManager = cardManager;
    private readonly IBuildingManager _buildingManager = buildingManager;

    public void Run(in GameTime state)
    {
        if (_cardManager.CapturedCard == null)
        {
            if (_buildingManager.CapturedBuilding != null)
            {
                _buildingManager.DespawnCapturedBuildingWithChildren();
            }
            return;
        }

        var windowHeight = _windowManager.ScreenHeight;
        var cardTexture = _cardManager.CapturedCard.Value.Get<Sprite>().Texture;
        var spriteHeight = cardTexture.Height * TextureSizeConstants.CardSpriteSize;
        var bottomBorder = windowHeight / 2.0f - spriteHeight + _camera.Position.Y;

        ref var cardVisibility = ref _cardManager.CapturedCard.Value.Get<VisibilityComponent>();
        var building = _cardManager.CapturedCard.Value.Get<BuildingComponent>();
        var mousePos = _mouseInput.GetMousePosition().ToVector2();

        if (mousePos.Y < bottomBorder)
        {
            if (cardVisibility.Visibility != Visibility.Hidden)
            {
                cardVisibility.Visibility = Visibility.Hidden;
                _buildingManager.SpawnCapturedBuilding(building.Building, mousePos);
            }
        }
        else
        {
            if (cardVisibility.Visibility != Visibility.Visible)
            {
                cardVisibility.Visibility = Visibility.Visible;
                _buildingManager.DespawnCapturedBuildingWithChildren();
            }
        }
    }
}
