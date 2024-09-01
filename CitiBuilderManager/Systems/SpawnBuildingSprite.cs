using Arch.Core;
using Arch.Core.Extensions;
using CitiBuilderManager.Components;
using CitiBuilderManager.Services;
using Engine.Attributes;
using Engine.Bundles;
using Engine.Components;
using Engine.Services;
using Engine.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace CitiBuilderManager.Systems;

[AutoInject]
[OnStartup]
public class SpawnBuildingSprite : ISystem<int>
{
    private readonly ILoader _loader;
    private readonly IWindowManager _window;
    private readonly World _world;

    public SpawnBuildingSprite(World world, ILoader loader, IWindowManager window)
    {
        _world = world;
        _window = window;
        _loader = loader;
    }

    public void Run(in int state)
    {
        var rand = new Random();

        for (int i = 0; i < 100; i++)
        {
            var position = new Vector3(rand.Next(_window.ScreenWidth), rand.Next(_window.ScreenHeight), 0);

            var spriteEntity = new SpriteBundle(
             new Sprite(_loader.Load<Texture2D>(AssetNames.Building)),
             new Transform2D()
             {
                 Translation = position,
                 Rotation = 0,
                 Scale = 0.2f,
             },
             Visibility.Visible
            )
            .Spawn(_world);

            spriteEntity.Add(new Velocity() { Direction = GetRandomDirection(rand), Speed = 100f });
        }
    }

    private Vector2 GetRandomDirection(Random rand)
    {
        float x = (float)rand.NextSingle() * 2.0f - 1.0f;
        float y = (float)rand.NextSingle() * 2.0f - 1.0f;

        var res = new Vector2(x, y);

        if (res != Vector2.Zero)
            res.Normalize();

        return res;
    }
}
