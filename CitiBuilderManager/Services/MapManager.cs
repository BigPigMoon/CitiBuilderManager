using System;
using System.Collections.Generic;
using Arch.Core;
using CitiBuilderManager.Interfaces;
using Engine.Components;
using Microsoft.Extensions.Logging;
using Microsoft.Xna.Framework;

namespace CitiBuilderManager.Services;

public class MapManager(World world, ILogger<MapManager> logger) : IMapManager
{
    private readonly HashSet<Vector2> _map = [];
    private readonly QueryDescription _childrenQuery = new QueryDescription().WithAll<Child>();
    private readonly World _world = world;
    private readonly ILogger<MapManager> _logger = logger;

    public bool IsBooked(in Vector2 position)
    {
        return _map.Contains(position);
    }

    public bool IsBooked(in Entity building)
    {
        var buildingParent = building;
        var isBooked = false;

        _world.Query(in _childrenQuery, (ref Child cube, ref Transform2D cubeTransform) =>
        {
            if (cube.Parent == buildingParent)
            {
                _logger.LogInformation("cube position: {}", cubeTransform.Position);

                if (IsBooked(cubeTransform.Position))
                {
                    isBooked = true;
                    return;
                }
            }
        });

        _logger.LogInformation("is booked: {}", isBooked);
        _logger.LogInformation("map: {}", _map);

        return isBooked;
    }

    public void AddBuilding(in Entity building)
    {
        var buildingParent = building;

        _world.Query(in _childrenQuery, (ref Child cube, ref Transform2D cubeTransform) =>
        {
            if (cube.Parent == buildingParent)
            {
                _map.Add(cubeTransform.Position);
            }
        });
    }

    public void RemoveBuilding(in Entity building)
    {
        throw new NotImplementedException();
    }
}
