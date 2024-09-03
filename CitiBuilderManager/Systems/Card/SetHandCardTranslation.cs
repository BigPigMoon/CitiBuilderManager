using System;
using System.Collections.Generic;
using System.Linq;
using Arch.Core;
using Arch.Core.Extensions;
using CitiBuilderManager.Components;
using CitiBuilderManager.Enums;
using Engine.Attributes;
using Engine.Components;
using Engine.Interfaces;
using Engine.Systems;
using Microsoft.Extensions.Logging;
using Microsoft.Xna.Framework;

namespace CitiBuilderManager.Systems;

[AutoInject]
[OnUpdate]
public class SetHandCardTranslation(World world, IWindowManager windowManager, ICamera2D camera, ILogger<SetHandCardTranslation> logger) : ISystem
{
    private readonly QueryDescription _queryDescription = new QueryDescription().WithAll<SmoothTransform, Sprite, Card>();
    private readonly World _world = world;
    private readonly IWindowManager _windowManager = windowManager;
    private readonly ICamera2D _camera = camera;
    private readonly ILogger<SetHandCardTranslation> _logger = logger;

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

        var cardSize = textureSize * firstCard.Get<Transform2D>().Scale;
        var selectedCardSize = textureSize * 0.5f;

        var halfTotalWidth = GetHalfTotalWidth(cardNum, cardSize.X, false);
        var halfTotalRotation = GetHalfTotalRotation(cardNum);

        cards.Sort((x, y) =>
        {
            var xOrder = x.Get<Card>().Order;
            var yOrder = y.Get<Card>().Order;

            return yOrder.CompareTo(xOrder);
        });

        for (int i = 0; i < cards.Count; i++)
        {
            var transform = cards[i].Get<SmoothTransform>();

            var t = cardNum > 1 ? (float)i / (cardNum - 1) : 0.0f;

            var angle = Single.Lerp(-halfTotalRotation, halfTotalRotation, t);

            var x = -Single.Lerp(halfTotalWidth, -halfTotalWidth, t);
            var y = Single.Sin(angle / 2.0f) * x - cardSize.Y / 3.0f + _windowManager.ScreenHeight * 0.5f;
            var z = 0.1f * (cardNum - i) + (float)SpriteLayersEnum.Card;

            transform = new SmoothTransform(new Vector2(x, y) + _camera.Position, angle, transform.Scale, z);

            cards[i].Set(transform);
        }
    }

    private static float GetHalfTotalWidth(int cardNum, float cardWidth, bool containSelected)
    {
        var totalWidth = (cardNum - 1) * (cardWidth * 0.8f) * (containSelected ? 1.15f : 1.0f);
        return totalWidth / 2.0f;
    }

    private static float GetHalfTotalRotation(int cardNum)
    {
        var rotationStep = (float)(cardNum * (Math.PI / 180));
        var totalRotation = (cardNum - 1) * rotationStep;
        return totalRotation / 2.0f;
    }
}
