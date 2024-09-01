using Arch.Core;
using Engine.Attributes;
using Engine.Components;
using Engine.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine.Systems;

[AutoInject]
[OnDraw]
public sealed class DrawSpriteSystem : ISystem<GameTime>
{
    private readonly QueryDescription _spriteQuery = new QueryDescription().WithAll<Sprite, Transform2D>();
    private readonly IDrawer _drawer;
    private readonly World _world;

    public DrawSpriteSystem(World world, IDrawer drawer)
    {
        _world = world;
        _drawer = drawer;
    }

    public void Run(in GameTime state)
    {
        _world.Query(in _spriteQuery, (ref Sprite sprite, ref Transform2D transform) =>
        {
            _drawer.Begin();

            var position = new Vector2(transform.Translation.X, transform.Translation.Y);

            SpriteEffects spriteEffect = SpriteEffects.None;
            spriteEffect |= sprite.FlipX ? SpriteEffects.FlipHorizontally : 0;
            spriteEffect |= sprite.FlipY ? SpriteEffects.FlipVertically : 0;

            // TODO: add source rect and origin
            _drawer.Draw(sprite.Texture, position, null, sprite.Color, transform.Rotation, Vector2.Zero, transform.Scale, spriteEffect, transform.Translation.Z);

            _drawer.End();
        });
    }
}
