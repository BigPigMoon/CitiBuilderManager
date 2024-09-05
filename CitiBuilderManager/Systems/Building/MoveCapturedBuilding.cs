using Arch.Core;
using CitiBuilderManager.Components;
using CitiBuilderManager.Constants;
using Engine.Attributes;
using Engine.Components;
using Engine.Interfaces;
using Engine.Systems;
using Microsoft.Extensions.Logging;
using Microsoft.Xna.Framework;

namespace CitiBuilderManager.Systems;

[AutoInject]
[OnUpdate]
public class MoveCapturedBuilding(World world, IMouseInput mouseInput, ILogger<MoveCapturedBuilding> logger) : ISystem
{
    private readonly QueryDescription _query = new QueryDescription().WithAll<Transform2D, Sprite, CubePositionComponent, CapturedCubeComponent>();
    private readonly World _world = world;
    private readonly IMouseInput _mouseInput = mouseInput;
    private readonly ILogger<MoveCapturedBuilding> _logger = logger;

    public void Run(in GameTime state)
    {
        var mousePosition = _mouseInput.GetMousePosition().ToVector2();

        _logger.LogInformation("====== LOOOP ======");

        _world.Query(in _query, (ref Transform2D transform, ref CubePositionComponent cubePosition, ref Sprite sprite) =>
        {
            var cubeTextureSize = new Vector2(sprite.Texture.Width, sprite.Texture.Height);
            var cubeSize = cubeTextureSize * TextureSizeConstants.WorldBuildingScale;

            var gridPosition = (mousePosition + cubePosition.Position * cubeSize) / cubeSize;
            gridPosition.Floor();
            var newCubePosition = (gridPosition + new Vector2(0.5f)) * cubeSize;
            transform.Position = newCubePosition;
            _logger.LogInformation("cube pos {}", newCubePosition);
        });
    }
}
