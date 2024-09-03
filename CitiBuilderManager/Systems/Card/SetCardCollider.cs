using Arch.Core;
using CitiBuilderManager.Components;
using Engine.Attributes;
using Engine.Components;
using Engine.Systems;
using Microsoft.Xna.Framework;

namespace CitiBuilderManager.Systems;

[AutoInject]
[OnUpdate]
internal class SetCardCollider(World world) : ISystem
{
    private readonly QueryDescription queryDescription = new QueryDescription().WithAll<BoxCollider, Transform2D, Sprite>();
    private readonly World _world = world;

    public void Run(in GameTime state)
    {
        _world.Query(in queryDescription, (ref BoxCollider collider, ref Transform2D transform, ref Sprite sprite) =>
        {
            var size = new Vector2(sprite.Texture.Width, sprite.Texture.Height) * transform.Scale;
            var halfSize = size / 2.0f;
            var angle = transform.Rotation;

            collider = new BoxCollider(transform.Position, angle, halfSize);
        });
    }
}
