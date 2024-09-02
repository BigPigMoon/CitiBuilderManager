using Arch.Core;
using CitiBuilderManager.Components;
using Engine.Attributes;
using Engine.Components;
using Engine.Interfaces;
using Engine.Systems;
using Microsoft.Xna.Framework;

namespace CitiBuilderManager.Systems;

[AutoInject]
[OnUpdate]
public class MoveCubeSystem(World world, IWindowManager windowManager) : ISystem<GameTime>
{
    private GameTime _gameTime;
    private readonly QueryDescription _queryDescription = new QueryDescription().WithAll<Transform2D, Velocity>();

    private readonly World _world = world;
    private readonly IWindowManager _windowManager = windowManager;

    public void Run(in GameTime state)
    {
        _gameTime = state;

        _world.Query(in _queryDescription, (Entity entity, ref Transform2D transform, ref Velocity velocity) =>
        {
            float deltaTime = (float)_gameTime.ElapsedGameTime.TotalSeconds;
            transform.Translation += new Vector3(Vector2.Multiply(velocity.Direction, deltaTime * velocity.Speed), transform.Translation.Z);
        });
    }
}
