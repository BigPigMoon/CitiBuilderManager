using System;
using System.Collections.Generic;
using Arch.Core;
using Arch.Core.Extensions;
using CitiBuilderManager.Components;
using CitiBuilderManager.Interfaces;
using Engine.Attributes;
using Engine.Interfaces;
using Engine.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CitiBuilderManager.Systems;

[AutoInject]
[OnUpdate]
public class DespawnRandomCard(World world, IKeyboardInput keyboardInput, ICardManager cardManager) : ISystem
{
    private readonly QueryDescription _query = new QueryDescription().WithAll<CardComponent>();
    private readonly World _world = world;
    private readonly IKeyboardInput _keyboardInput = keyboardInput;
    private readonly ICardManager _cardManager = cardManager;

    public void Run(in GameTime state)
    {
        var random = new Random();
        var cards = new List<Entity>();

        if (_keyboardInput.IsKeyJustPressed(Keys.Delete))
        {
            _world.GetEntities(in _query, cards);
            int index = random.Next(cards.Count);
            _cardManager.DespawnCard(cards[index]);
        }
    }
}
