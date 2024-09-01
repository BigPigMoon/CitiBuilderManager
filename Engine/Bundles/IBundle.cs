using Arch.Core;

namespace Engine.Bundles;

public interface IBundle
{
    Entity Spawn(World world);
}