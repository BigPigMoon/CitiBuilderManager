using Arch.Core.Extensions;
using CitiBuilderManager.Constants;
using CitiBuilderManager.Interfaces;
using Engine.Attributes;
using Engine.Components;
using Engine.Interfaces;
using Engine.Systems;
using Microsoft.Xna.Framework;

namespace CitiBuilderManager.Systems;

[AutoInject]
[OnUpdate]
public class MoveCapturedCard(IMouseInput mouseInput, ICardManager cardManager) : ISystem
{
    private readonly IMouseInput _mouseInput = mouseInput;
    private readonly ICardManager _cardManager = cardManager;

    public void Run(in GameTime state)
    {
        if (_cardManager.CapturedCard == null)
            return;

        ref var transform = ref _cardManager.CapturedCard.Value.Get<Transform2D>();
        transform.Position = _mouseInput.GetMousePosition().ToVector2();
        transform.Rotation = 0;
        transform.Scale = TextureSizeConstants.SelectedCardSpriteSize;
    }
}
