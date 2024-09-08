using System.Collections.Generic;
using Arch.Core;
using CitiBuilderManager.GameObjects;
using Microsoft.Xna.Framework;

namespace CitiBuilderManager.Interfaces;

public interface IBuildingManager
{
    Entity? CapturedBuilding { get; }

    IEnumerable<Vector2> GetCubesPosition(BuildingGameObject building);
    IEnumerable<Entity> SpawnBuildingCubes(BuildingGameObject building, float cubeScale, Vector2 offset, float zLayer);
    Entity? SpawnCapturedBuilding(BuildingGameObject building, Vector2 position);
    void DespawnCapturedBuilding();
    void DespawnCapturedBuildingWithChildren();
}
