using Arch.Core;
using Microsoft.Xna.Framework;

namespace CitiBuilderManager.Interfaces;

public interface IMapManager
{
    bool IsBooked(in Vector2 position);
    bool IsBooked(in Entity building);
    void AddBuilding(in Entity building);
    void RemoveBuilding(in Entity building);
}
