using Arch.Core;
using CitiBuilderManager.Components;
using CitiBuilderManager.Constants;
using CitiBuilderManager.Enums;
using Engine.Attributes;
using Engine.Components;
using Engine.Interfaces;
using Engine.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CitiBuilderManager.Systems;

[AutoInject]
[OnUpdate]
public class MoveCapturedBuilding(World world, ILoader loader, IMouseInput mouseInput) : ISystem
{
    private readonly QueryDescription _query = new QueryDescription().WithAll<CapturedBuildingComponent, BuildingComponent, Transform2D>();
    private readonly World _world = world;
    private readonly ILoader _loader = loader;
    private readonly IMouseInput _mouseInput = mouseInput;

    public void Run(in GameTime state)
    {
        _world.Query(in _query, (ref Transform2D transform, ref BuildingComponent building) =>
        {
            var mousePosition = _mouseInput.GetMousePosition().ToVector2();
            var texture = _loader.Load<Texture2D>(AssetNamesEnum.Building);
            var cubeTextureSize = new Vector2(texture.Width, texture.Height);
            var cubeSize = cubeTextureSize * TextureSizeConstants.WorldBuildingScale;

            var offset = new Vector2(
                building.BuildingWidth % 2 == 0 ? 0.5f : 0f,
                building.BuildingHeight % 2 == 0 ? 0.5f : 0f
            );
            offset.Rotate(transform.Rotation);

            var gridPosition = (mousePosition - offset * cubeSize) / cubeSize;
            gridPosition.Floor();
            offset += new Vector2(0.5f); // need for correct mouse-building position
            transform.Position = (gridPosition + offset) * cubeSize;
        });
    }
}
