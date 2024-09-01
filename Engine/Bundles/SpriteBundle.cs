﻿using Arch.Core;
using Engine.Components;

namespace Engine.Bundles;

public sealed class SpriteBundle(Sprite spriteComponent, Transform2D transformComponent, Visibility visibilityComponent) : IBundle
{
    public Sprite SpriteComponent { get; set; } = spriteComponent;
    public Transform2D TransformComponent { get; set; } = transformComponent;
    public Visibility VisibilityComponent { get; set; } = visibilityComponent;

    public Entity Spawn(World world)
    {
        return world.Create(SpriteComponent, TransformComponent, VisibilityComponent);
    }
}
