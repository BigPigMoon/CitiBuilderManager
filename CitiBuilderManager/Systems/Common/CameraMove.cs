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
internal class CameraMove(World world, ICamera2D camera, IKeyboardInput keyboardInput) : ISystem
{
    private readonly QueryDescription _queryUI = new QueryDescription().WithAll<Transform2D, UIComponent>().WithNone<SmoothTransformComponent>();
    private readonly QueryDescription _queryAnimatedUI = new QueryDescription().WithAll<Transform2D, SmoothTransformComponent, UIComponent>();
    private readonly World _world = world;
    private readonly ICamera2D _camera = camera;
    private readonly IKeyboardInput _keyboardInput = keyboardInput;

    private readonly float _cameraSpeed = 300.0f;

    public void Run(in GameTime state)
    {
        float deltaTime = (float)state.ElapsedGameTime.TotalSeconds;
        var dir = Vector2.Zero;

        if (_keyboardInput.IsKeyPressed(Keys.A))
            dir.X -= 1;
        if (_keyboardInput.IsKeyPressed(Keys.W))
            dir.Y -= 1;
        if (_keyboardInput.IsKeyPressed(Keys.S))
            dir.Y += 1;
        if (_keyboardInput.IsKeyPressed(Keys.D))
            dir.X += 1;

        if (dir != Vector2.Zero)
            dir.Normalize();

        var deltaMoving = dir * _cameraSpeed * deltaTime;

        _camera.Position += deltaMoving;

        _world.Query(in _queryUI, (ref Transform2D transform) =>
        {
            transform.Position += deltaMoving;
        });

        _world.Query(in _queryAnimatedUI, (ref Transform2D transform, ref SmoothTransformComponent smoothTransform) =>
        {
            transform.Position += deltaMoving;
            smoothTransform.Position += deltaMoving;
        });
    }
}
