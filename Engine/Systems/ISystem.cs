using Microsoft.Xna.Framework;

namespace Engine.Systems;

public interface ISystem
{
    void Run(in GameTime state);
}
