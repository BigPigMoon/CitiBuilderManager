using System.Text;
using Arch.Core;
using Engine.Attributes;
using Engine.Components;
using Engine.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine.Systems;

[AutoInject]
[OnDraw]
public sealed class DrawSpriteSystem(World world, IDrawer drawer, ICamera2D camera) : ISystem
{
    private readonly QueryDescription _spriteQuery = new QueryDescription().WithAll<Sprite, Transform2D>();
    private readonly IDrawer _drawer = drawer;
    private readonly ICamera2D _camera = camera;
    private readonly World _world = world;

    public void Run(in GameTime state)
    {
        _drawer.Begin(transformMatrix: _camera.Transform, sortMode: SpriteSortMode.FrontToBack);
        _world.Query(in _spriteQuery, (Entity entity, ref Sprite sprite, ref Transform2D transform) =>
        {

            SpriteEffects spriteEffect = SpriteEffects.None;
            spriteEffect |= sprite.FlipX ? SpriteEffects.FlipHorizontally : 0;
            spriteEffect |= sprite.FlipY ? SpriteEffects.FlipVertically : 0;

            var origin = new Vector2(sprite.Texture.Width, sprite.Texture.Height) / 2.0f;

            // TODO: add source rect and origin
            _drawer.Draw(sprite.Texture, transform.Position, null, sprite.Color, transform.Rotation, origin, transform.Scale, spriteEffect, transform.Depth / 1000);
        });
        _drawer.End();
    }
}
