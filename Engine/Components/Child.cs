using System;
using Arch.Core;

namespace Engine.Components;

public class Child(Entity parent)
{
    public Entity Parent { get; set; } = parent;
}
