using System.Collections.Generic;
using Arch.Core;
using Arch.Core.Extensions;
using CitiBuilderManager.Components;
using CitiBuilderManager.Constants;
using CitiBuilderManager.Enums;
using CitiBuilderManager.GameObjects;
using CitiBuilderManager.Interfaces;
using Engine.Bundles;
using Engine.Components;
using Engine.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CitiBuilderManager.Services;

public class CardManager(World world, ILoader loader, IWindowManager window, ICamera2D camera, IBuildingManager buildingManager, ILogger<CardManager> logger) : ICardManager
{
    private readonly QueryDescription _cardQuery = new QueryDescription().WithAll<CardComponent>();
    private readonly World _world = world;
    private readonly ILoader _loader = loader;
    private readonly ICamera2D _camera = camera;
    private readonly IBuildingManager _buildingManager = buildingManager;
    private readonly IWindowManager _window = window;
    private readonly ILogger<CardManager> _logger = logger;

    public Entity? SelectedCard { get; set; }
    public Entity? CapturedCard { get; set; }

    public Entity SpawnCard()
    {
        _logger.LogInformation("Spawning new card");

        var offset = 80.0f;
        var spawnPoint = new Vector2(_window.ScreenWidth / -2.0f, _window.ScreenHeight / 2.0f) + _camera.Position + new Vector2(offset, -offset);
        var spawnZ = (float)SpriteLayersEnum.Card;

        var count = _world.CountEntities(in _cardQuery);

        var rotation = 0.0f;
        var texture = _loader.Load<Texture2D>(AssetNamesEnum.EmptyCard);
        var halfSize = Vector2.Divide(new Vector2(texture.Width, texture.Height), 2.0f);

        var card = new SpriteBundle(
            spriteComponent: new Sprite(texture),
            transformComponent: new Transform2D(spawnPoint, rotation, TextureSizeConstants.CardSpriteSize, spawnZ),
            visibilityComponent: Visibility.Visible
        ).Spawn(_world);

        card.Add(new CardComponent(count + 1));
        card.Add(new BoxColliderComponent(spawnPoint, rotation, halfSize));
        card.Add(new SmoothTransformComponent(spawnPoint, rotation, TextureSizeConstants.CardSpriteSize, spawnZ));
        card.Add(new UIComponent());

        var building = new BuildingGameObject();
        var cubes = _buildingManager.SpawnBuildingCubes(building, 0.15f, new Vector2(0f, -100f), 0.1f);
        foreach (var cube in cubes)
        {
            cube.Add(new Child(card));
        }

        return card;
    }

    public void DespawnCard(Entity card)
    {
        var cards = new List<Entity>();
        _world.GetEntities(in _cardQuery, cards);

        _world.Destroy(card);
        cards.Remove(card);

        cards.Sort((x, y) =>
        {
            var xOrder = x.Get<CardComponent>().Order;
            var yOrder = y.Get<CardComponent>().Order;

            return xOrder.CompareTo(yOrder);
        });

        for (int i = 0; i < cards.Count; i++)
        {
            ref var order = ref cards[i].Get<CardComponent>();

            order.Order = i;
        }
    }
}
