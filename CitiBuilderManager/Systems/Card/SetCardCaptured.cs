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
public class SetCardCaptured(IMouseInput mouseInput, ICardManager cardManager) : ISystem
{
    private readonly IMouseInput _mouseInput = mouseInput;
    private readonly ICardManager _cardManager = cardManager;

    public void Run(in GameTime state)
    {
        if (_cardManager.SelectedCard == null)
            return;

        if (_mouseInput.IsKeyPressed(Engine.Enums.MouseButtonEnum.Left))
        {
            _cardManager.CapturedCard = _cardManager.SelectedCard;
            _cardManager.CapturedCard.Value.Remove<SmoothTransform>();
        }
        else
        {
            if (_cardManager.CapturedCard != null)
            {
                var transform = _cardManager.CapturedCard.Value.Get<Transform2D>();
                _cardManager.CapturedCard.Value.Add(new SmoothTransform(transform.Position, transform.Rotation, transform.Scale, transform.Depth));
            }
            _cardManager.CapturedCard = null;
        }
    }
}
