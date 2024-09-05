using Arch.Core;
using Arch.Core.Extensions;
using CitiBuilderManager.Components;
using CitiBuilderManager.Constants;
using CitiBuilderManager.GameObjects;
using CitiBuilderManager.Interfaces;
using Engine.Attributes;
using Engine.Interfaces;
using Engine.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CitiBuilderManager.Systems.Building;

[AutoInject]
[OnUpdate]
public class RegenBuilding(World world, IBuildingManager buildingManager, IKeyboardInput keyboardInput, IMouseInput mouseInput) : ISystem
{
    private readonly QueryDescription _query = new QueryDescription().WithAll<CapturedCubeComponent>();
    private readonly World _world = world;
    private readonly IBuildingManager _buildingManager = buildingManager;
    private readonly IKeyboardInput _keyboardInput = keyboardInput;
    private readonly IMouseInput _mouseInput = mouseInput;

    public void Run(in GameTime state)
    {
        if (_keyboardInput.IsKeyJustPressed(Keys.B))
        {
            _world.Query(in _query, (Entity cube) =>
            {
                cube.Remove<CapturedCubeComponent>();
            });

            var building = new BuildingGameObject();

            var cubes = _buildingManager.SpawnBuildingCubes(building, TextureSizeConstants.WorldBuildingScale, _mouseInput.GetMousePosition().ToVector2(), 20f);

            foreach (var cube in cubes)
            {
                cube.Add<CapturedCubeComponent>();
            }
        }
    }
}
