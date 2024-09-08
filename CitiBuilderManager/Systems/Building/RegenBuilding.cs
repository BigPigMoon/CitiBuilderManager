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
    private readonly QueryDescription _childrenQuery = new QueryDescription().WithAll<Child>();
    private readonly World _world = world;
    private readonly IBuildingManager _buildingManager = buildingManager;
    private readonly IKeyboardInput _keyboardInput = keyboardInput;
    private readonly IMouseInput _mouseInput = mouseInput;

    public void Run(in GameTime state)
    {
        if (_keyboardInput.IsKeyJustPressed(Keys.B))
        {
            var building = new BuildingGameObject();
            _buildingManager.SpawnCapturedBuilding(building, _mouseInput.GetMousePosition().ToVector2());
        }
    }
}
