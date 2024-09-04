using Arch.Core;
using CitiBuilderManager.Enums;
using Engine.Bundles;
using Engine.Interfaces;
using Engine.Systems;
using Engine.Components;
using Microsoft.Extensions.Logging;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Engine.Attributes;
using Arch.Core.Extensions;
using CitiBuilderManager.Components;

namespace CitiBuilderManager.Systems;

[AutoInject]
// [OnStartup]
internal class SpawnCube(World world, ILoader loader, ILogger<SpawnCube> logger) : ISystem
{
    private readonly World _world = world;
    private readonly ILoader _loader = loader;
    private readonly ILogger<SpawnCube> _logger = logger;

    public void Run(in GameTime state)
    {
        var cubeSprite = new Sprite(_loader.Load<Texture2D>(AssetNamesEnum.Building));

        new SpriteBundle(
            spriteComponent: cubeSprite,
            transformComponent: new Transform2D(Vector2.Zero, 0, 0.3f, 3),
            visibilityComponent: Visibility.Visible
            ).Spawn(_world);

        cubeSprite.Color = Color.Red;

        var cube = new SpriteBundle(
            spriteComponent: cubeSprite,
            transformComponent: new Transform2D(Vector2.Zero, 0, 0.3f, 1),
            visibilityComponent: Visibility.Visible
            ).Spawn(_world);

        cube.Add<UIComponent>();
    }
}
