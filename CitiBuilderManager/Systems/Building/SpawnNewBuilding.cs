using Arch.Core.Extensions;
using CitiBuilderManager.Components;
using CitiBuilderManager.GameObjects;
using CitiBuilderManager.Interfaces;
using Engine.Attributes;
using Engine.Systems;
using Microsoft.Xna.Framework;

namespace CitiBuilderManager.Systems;

[AutoInject]
[OnStartup]
public class SpawnNewBuilding(IBuildingManager buildingManager) : ISystem
{
    private readonly IBuildingManager _buildingManager = buildingManager;

    public void Run(in GameTime state)
    {
        var building = new Building();

        var cubes = _buildingManager.SpawnBuildingCubes(building, 0.1f, Vector2.Zero, 20f);

        foreach (var cube in cubes)
        {
            cube.Add<BuildingCapturedComponent>();
        }
    }
}
