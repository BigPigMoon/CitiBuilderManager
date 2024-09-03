using Arch.Core;
using CitiBuilderManager.Components;
using Engine.Attributes;
using Engine.Components;
using Engine.Systems;
using Microsoft.Xna.Framework;
using System;

namespace CitiBuilderManager.Systems;

[AutoInject]
[OnUpdate]
internal class SmoothMovement(World world) : ISystem
{
    private readonly QueryDescription _desc = new QueryDescription().WithAll<Transform2D, SmoothTransform>();
    private readonly World _world = world;
    private readonly float _animationSpeed = 5.0f;

    public void Run(in GameTime state)
    {
        var gameTime = state;

        _world.Query(in _desc, (ref Transform2D transform, ref SmoothTransform smoothTransform) =>
        {
            float t = (float)gameTime.ElapsedGameTime.TotalSeconds * _animationSpeed;

            var translation = Vector2.Lerp(transform.Position, smoothTransform.Translation, t);
            var rotation = Single.Lerp(transform.Rotation, smoothTransform.Rotation, t);
            var scale = Single.Lerp(transform.Scale, smoothTransform.Scale, t);
            var depth = Single.Lerp(transform.Depth, smoothTransform.Depth, t);

            transform = new Transform2D(translation, rotation, scale, depth);
        });
    }
}
