using Arch.Core.Extensions;
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
public class RotateCapturedBuilding(IKeyboardInput keyboardInput, IBuildingManager buildingManager) : ISystem
{
    private readonly IKeyboardInput _keyboardInput = keyboardInput;
    private readonly IBuildingManager _buildingManager = buildingManager;

    public void Run(in GameTime state)
    {
        if (_buildingManager.CapturedBuilding != null && _keyboardInput.IsKeyJustPressed(Keys.R))
        {
            ref var transform = ref _buildingManager.CapturedBuilding.Value.Get<Transform2D>();
            transform.Rotation += float.DegreesToRadians(90f);
        }
    }
}
