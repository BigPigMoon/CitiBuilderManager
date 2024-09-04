using System;
using Arch.Core;

namespace CitiBuilderManager.Interfaces;

public interface ICardManager
{
    Entity? SelectedCard { get; set; }
    Entity? CapturedCard { get; set; }
    Entity SpawnCard();
    void DespawnCard(Entity card);
}
