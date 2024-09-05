using System.Collections.Generic;
using Arch.Core;
using Arch.Core.Extensions;
using CitiBuilderManager.Components;
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

public class BuildingManager(World world, ILoader loader, ILogger<BuildingManager> logger) : IBuildingManager
{
    private readonly World _world = world;
    private readonly ILoader _loader = loader;
    private readonly ILogger<BuildingManager> _logger = logger;

    public IEnumerable<Vector2> GetCubesPosition(BuildingGameObject building)
    {
        var result = new List<Vector2>();

        var widthRange = building.GetClearColumns();
        var mapWidht = widthRange.End.Value - widthRange.Start.Value;

        var heightRange = building.GetClearRows();
        var mapHeight = heightRange.End.Value - heightRange.Start.Value;

        for (var y = 0; y < mapHeight; y++)
        {
            for (var x = 0; x < mapWidht; x++)
            {
                if (!building.Map[y + heightRange.Start.Value, x + widthRange.Start.Value])
                {
                    continue;
                }

                var dx = 0.5f;// mapWidht % 2 == 0 ? 0.5f : 0.0f;
                var dy = 0.5f;// mapHeight % 2 == 0 ? 0.5f : 0.0f;

                var cubePos = new Vector2(
                    x - mapWidht / 2.0f + dx,
                    y - mapHeight / 2.0f + dy
                );
                result.Add(cubePos);
            }
        }

        return result;
    }

    public IEnumerable<Entity> SpawnBuildingCubes(BuildingGameObject building, float cubeScale, Vector2 offset, float zLayer)
    {
        var cubesPosition = GetCubesPosition(building);
        var result = new List<Entity>();
        var cubeTexture = _loader.Load<Texture2D>(AssetNamesEnum.Building);
        var imageSize = new Vector2(cubeTexture.Width, cubeTexture.Height) * cubeScale;

        foreach (var position in cubesPosition)
        {
            var cube = new SpriteBundle(
                spriteComponent: new Sprite(cubeTexture),
                transformComponent: new Transform2D(position * imageSize + offset, 0, cubeScale, zLayer),
                visibilityComponent: Visibility.Visible
            ).Spawn(_world);
            cube.Add(new CubePositionComponent(position));

            _logger.LogInformation("cube position {}", position);

            result.Add(cube);
        }

        return result;
    }
}
