using Arch.Core;
using Arch.Core.Extensions;
using CitiBuilderManager.Components;
using CitiBuilderManager.Enums;
using CitiBuilderManager.Interfaces;
using Engine.Bundles;
using Engine.Components;
using Engine.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CitiBuilderManager.Services;

internal class CardSpawner(World world, ILoader loader, ILogger<CardSpawner> logger, IWindowManager window, ICamera2D camera) : ICardSpawner
{
    private readonly QueryDescription _query = new QueryDescription().WithAll<Card>();
    private readonly World _world = world;
    private readonly ILoader _loader = loader;
    private readonly ICamera2D _camera = camera;
    private readonly IWindowManager _window = window;
    private readonly ILogger<CardSpawner> _logger = logger;

    public Entity SpawnCard()
    {
        _logger.LogInformation("Spawning new card");

        // let spawn_point = (-window_half_size) + camera.translation.xy() + Vec2::splat(offset);
        var offset = 80.0f;
        var spawnPoint = new Vector2(_window.ScreenWidth / -2.0f, _window.ScreenHeight / 2.0f) + _camera.Position + new Vector2(offset, -offset);

        var count = _world.CountEntities(in _query);

        var rotation = 0.0f;
        var texture = _loader.Load<Texture2D>(AssetNamesEnum.EmptyCard);
        var halfSize = Vector2.Divide(new Vector2(texture.Width, texture.Height), 2.0f);

        var card = new SpriteBundle(
            spriteComponent: new Sprite(texture),
            transformComponent: new Transform2D(spawnPoint, rotation, 0.5f, 0.0f),
            visibilityComponent: Visibility.Visible
        ).Spawn(_world);

        card.Add(new Card(count));
        card.Add(new BoxCollider(spawnPoint, rotation, halfSize));
        card.Add(new SmoothTransform(spawnPoint, rotation, 0.5f, 0.0f));
        card.Add(new UIComponent());

        return card;
    }
}
