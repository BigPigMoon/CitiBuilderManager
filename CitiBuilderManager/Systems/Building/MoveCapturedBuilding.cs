using Arch.Core;
using Arch.Core.Extensions;
using CitiBuilderManager.Components;
using CitiBuilderManager.Constants;
using CitiBuilderManager.Enums;
using CitiBuilderManager.Interfaces;
using Engine.Attributes;
using Engine.Components;
using Engine.Interfaces;
using Engine.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CitiBuilderManager.Systems;

[AutoInject]
[OnUpdate]
public class MoveCapturedBuilding(ILoader loader, IMouseInput mouseInput, IBuildingManager buildingManager) : ISystem
{
    private readonly ILoader _loader = loader;
    private readonly IMouseInput _mouseInput = mouseInput;
    private readonly IBuildingManager _buildingManager = buildingManager;

    public void Run(in GameTime state)
    {
        if (_buildingManager.CapturedBuilding != null)
        {
            ref var transform = ref _buildingManager.CapturedBuilding.Value.Get<Transform2D>();
            var building = _buildingManager.CapturedBuilding.Value.Get<BuildingComponent>();

            var mousePosition = _mouseInput.GetMousePosition().ToVector2();
            var texture = _loader.Load<Texture2D>(AssetNamesEnum.Building);
            var cubeTextureSize = new Vector2(texture.Width, texture.Height);
            var cubeSize = cubeTextureSize * TextureSizeConstants.WorldBuildingScale;

            var offset = new Vector2(
                            building.Building.Widht % 2 == 0 ? 0.5f : 0f,
                            building.Building.Height % 2 == 0 ? 0.5f : 0f
                        );
            offset.Rotate(transform.Rotation);

            var gridPosition = (mousePosition - offset * cubeSize) / cubeSize;
            gridPosition.Floor();
            offset += new Vector2(0.5f); // need for correct mouse-building position
            transform.Position = (gridPosition + offset) * cubeSize;
        }
    }
}
