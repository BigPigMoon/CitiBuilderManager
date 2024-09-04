using System;
using System.Collections.Generic;
using System.Linq;
using Arch.Core;
using Arch.Core.Extensions;
using CitiBuilderManager.Components;
using CitiBuilderManager.Constants;
using CitiBuilderManager.Enums;
using CitiBuilderManager.Interfaces;
using Engine.Attributes;
using Engine.Components;
using Engine.Interfaces;
using Engine.Systems;
using Microsoft.Xna.Framework;

namespace CitiBuilderManager.Systems;

[AutoInject]
[OnUpdate]
public class SetHandCardTranslation(World world, IWindowManager windowManager, ICamera2D camera, ICardManager cardManager) : ISystem
{
    private readonly QueryDescription _queryDescription = new QueryDescription().WithAll<SmoothTransformComponent, Sprite, CardComponent>();
    private readonly World _world = world;
    private readonly IWindowManager _windowManager = windowManager;
    private readonly ICamera2D _camera = camera;
    private readonly ICardManager _cardManager = cardManager;

    public void Run(in GameTime state)
    {
        var cards = new List<Entity>();
        _world.GetEntities(_queryDescription, cards);

        var cardNum = cards.Count;
        if (cardNum == 0)
            return;

        var firstCard = cards.First();

        var cardTexture = firstCard.Get<Sprite>().Texture;
        var textureSize = new Vector2(cardTexture.Width, cardTexture.Height);

        var cardSize = textureSize * TextureSizeConstants.CardSpriteSize;
        var selectedCardSize = textureSize * TextureSizeConstants.SelectedCardSpriteSize;

        var halfTotalWidth = GetHalfTotalWidth(cardNum, cardSize.X, _cardManager.SelectedCard != null);
        var halfTotalRotation = GetHalfTotalRotation(cardNum);

        cards.Sort((x, y) =>
        {
            var xOrder = x.Get<CardComponent>().Order;
            var yOrder = y.Get<CardComponent>().Order;

            return yOrder.CompareTo(xOrder);
        });

        for (int i = 0; i < cards.Count; i++)
        {
            ref var transform = ref cards[i].Get<SmoothTransformComponent>();

            var t = cardNum > 1 ? (float)i / (cardNum - 1) : 0.0f;

            var angle = float.Lerp(-halfTotalRotation, halfTotalRotation, t);

            var x = -float.Lerp(halfTotalWidth, -halfTotalWidth, t);
            var y = float.Sin(angle / 2.0f) * x - cardSize.Y / 3.0f + _windowManager.ScreenHeight * 0.5f;
            var z = 0.01f * i + (float)SpriteLayersEnum.Card;

            transform.Position = new Vector2(x, y) + _camera.Position;
            transform.Rotation = angle;
            transform.Scale = TextureSizeConstants.CardSpriteSize;
            transform.Depth = z;
        }

        if (_cardManager.SelectedCard != null && _cardManager.SelectedCard.Value.Has<SmoothTransformComponent>())
        {
            ref var transform = ref _cardManager.SelectedCard.Value.Get<SmoothTransformComponent>();
            var newPosition = transform.Position;
            newPosition.Y = (_windowManager.ScreenHeight / 2.0f) - (selectedCardSize / 2.0f + _camera.Position).Y;

            transform.Position = newPosition;
            transform.Scale = TextureSizeConstants.SelectedCardSpriteSize;
            transform.Depth = 0.01f * (cardNum + 1) + (float)SpriteLayersEnum.Card;
            transform.Rotation = 0;
        }
    }

    private static float GetHalfTotalWidth(int cardNum, float cardWidth, bool containSelected)
    {
        var totalWidth = (cardNum - 1) * (cardWidth * 0.8f) * (containSelected ? 1.1f : 1.0f);
        return totalWidth / 2.0f;
    }

    private static float GetHalfTotalRotation(int cardNum)
    {
        var rotationStep = (float)(cardNum * (Math.PI / 180));
        var totalRotation = (cardNum - 1) * rotationStep;
        return totalRotation / 2.0f;
    }
}
