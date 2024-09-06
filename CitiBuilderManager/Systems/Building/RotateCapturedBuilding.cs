using Arch.Core;
using CitiBuilderManager.Components;
using Engine.Attributes;
using Engine.Components;
using Engine.Interfaces;
using Engine.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CitiBuilderManager.Systems;

[AutoInject]
[OnUpdate]
public class RotateCapturedBuilding(World world, IKeyboardInput keyboardInput) : ISystem
{
    private readonly QueryDescription _query = new QueryDescription().WithAll<CapturedBuildingComponent, Transform2D>();
    private readonly World _world = world;
    private readonly IKeyboardInput _keyboardInput = keyboardInput;

    public void Run(in GameTime state)
    {
        if (_keyboardInput.IsKeyJustPressed(Keys.R))
        {
            _world.Query(in _query, (ref Transform2D transform) =>
            {
                transform.Rotation += float.DegreesToRadians(90f);
            });
        }
    }
}
