using Arch.Core;
using Arch.Core.Extensions;
using CitiBuilderManager.Components;
using CitiBuilderManager.Constants;
using CitiBuilderManager.GameObjects;
using CitiBuilderManager.Interfaces;
using Engine.Attributes;
using Engine.Components;
using Engine.Interfaces;
using Engine.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CitiBuilderManager.Systems;

[AutoInject]
[OnUpdate]
public class RegenBuilding(World world, IBuildingManager buildingManager, IKeyboardInput keyboardInput, IMouseInput mouseInput) : ISystem
{
    private readonly QueryDescription _capturedBuildingQuery = new QueryDescription().WithAll<CapturedBuildingComponent>();
    private readonly QueryDescription _childrenQuery = new QueryDescription().WithAll<Child>();
    private readonly World _world = world;
    private readonly IBuildingManager _buildingManager = buildingManager;
    private readonly IKeyboardInput _keyboardInput = keyboardInput;
    private readonly IMouseInput _mouseInput = mouseInput;

    public void Run(in GameTime state)
    {
        if (_keyboardInput.IsKeyJustPressed(Keys.B))
        {
            _world.Query(_capturedBuildingQuery, (Entity capturedBuilding) =>
            {
                _world.Query(in _childrenQuery, (Entity entity, ref Child child) =>
                {
                    if (child.Parent == capturedBuilding)
                    {
                        entity.Remove<Child>();
                    }
                });
                _world.Destroy(capturedBuilding);
            });

            var building = new BuildingGameObject();
            var cubes = _buildingManager.SpawnBuildingCubes(building, TextureSizeConstants.WorldBuildingScale, Vector2.Zero, 20f);

            var capturedBuildingComponent = _world.Create(
                    new Transform2D(_mouseInput.GetMousePosition().ToVector2(), 0, 1f, 20f),
                    new CapturedBuildingComponent(),
                    new BuildingComponent(building.Widht, building.Height)
                );

            foreach (var cube in cubes)
            {
                cube.Add(new Child(capturedBuildingComponent));
            }
        }
    }
}
