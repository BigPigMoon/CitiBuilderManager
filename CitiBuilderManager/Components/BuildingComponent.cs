using CitiBuilderManager.GameObjects;

namespace CitiBuilderManager.Components;

public class BuildingComponent(BuildingGameObject building)
{
    public BuildingGameObject Building { get; set; } = building;
}
