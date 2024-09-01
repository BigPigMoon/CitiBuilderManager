using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine.Components;

public struct Sprite(Texture2D texture)
{
    public Texture2D Texture { get; set; } = texture;
    public Color Color { get; set; } = Color.White;
    public bool FlipX { get; set; } = false;
    public bool FlipY { get; set; } = false;
}
