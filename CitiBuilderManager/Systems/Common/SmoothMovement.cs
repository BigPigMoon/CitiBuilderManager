using Arch.Core;
using CitiBuilderManager.Components;
using Engine.Attributes;
using Engine.Components;
using Engine.Systems;
using Microsoft.Xna.Framework;

namespace CitiBuilderManager.Systems;

[AutoInject]
[OnUpdate]
internal class SmoothMovement(World world) : ISystem
{
    private readonly QueryDescription _desc = new QueryDescription().WithAll<Transform2D, SmoothTransformComponent>();
    private readonly World _world = world;
    private readonly float _animationSpeed = 7.0f;

    public void Run(in GameTime state)
    {
        var gameTime = state;

        _world.Query(in _desc, (ref Transform2D transform, ref SmoothTransformComponent smoothTransform) =>
        {
            float t = (float)gameTime.ElapsedGameTime.TotalSeconds * _animationSpeed;

            var translation = Vector2.Lerp(transform.Position, smoothTransform.Position, t);
            var rotation = float.Lerp(transform.Rotation, smoothTransform.Rotation, t);
            var scale = float.Lerp(transform.Scale, smoothTransform.Scale, t);
            var depth = float.Lerp(transform.Depth, smoothTransform.Depth, t);

            transform = new Transform2D(translation, rotation, scale, depth);
        });
    }
}
