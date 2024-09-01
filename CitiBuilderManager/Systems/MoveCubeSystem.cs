using Arch.Core;
using CitiBuilderManager.Components;
using Engine.Attributes;
using Engine.Components;
using Engine.Systems;
using Microsoft.Xna.Framework;
using System;

namespace CitiBuilderManager.Systems;

[AutoInject]
[OnUpdate]
public class MoveCubeSystem : ISystem<GameTime>
{
    private readonly World _world;
    private readonly QueryDescription _queryDescription = new QueryDescription().WithAll<Transform2D, Velocity>();
    private GameTime _gameTime;

    public MoveCubeSystem(World world)
    {
        _world = world;
    }

    public void Run(in GameTime state)
    {
        _gameTime = state;

        _world.Query(in _queryDescription, (ref Transform2D transform, ref Velocity velocity) =>
        {
            float deltaTime = (float)_gameTime.ElapsedGameTime.TotalSeconds;

            transform.Translation += new Vector3(Vector2.Multiply(velocity.Direction, deltaTime * velocity.Speed), transform.Translation.Z);
        });
    }
}
