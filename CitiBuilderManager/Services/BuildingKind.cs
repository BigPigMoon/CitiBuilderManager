﻿using Microsoft.Xna.Framework;
using System;

namespace CitiBuilderManager.Services;

public enum BuildingKinds
{
    ResidentialBuildings,    // жилиые здания
    EducationalInstitutions, // образовательные
    MedicalFacilities,       // медицинские
    CommercialProperties,    // торговные
    CulturalInstitutions,    // культурные
    RecreationalAreas,       // места отдыха
    IndustrialFacilities,    // промка
    GovernmentInstitutions,  // гос уч
}

public class BuildingKind
{
    public readonly BuildingKinds Kind;

    public BuildingKind()
    {
        Kind = GetRandom();
    }

    private static BuildingKinds GetRandom()
    {
        var values = Enum.GetValues(typeof(BuildingKinds));
        var random = new Random();
        var randomIndex = random.Next(values.Length);

        return (BuildingKinds)values.GetValue(randomIndex);
    }

    public Color GetColor()
    {
        return Color.White;
    }
}