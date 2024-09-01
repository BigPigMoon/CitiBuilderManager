using Engine.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace CitiBuilderManager.Services;

public class Building
{
    private const uint MapWidth = 3;
    private const uint MapHeight = 3;

    private readonly bool[,] _map = new bool[MapWidth, MapWidth];
    private readonly BuildingKind _kind = new();

    public Building()
    {
        GenerateMap();
    }

    public Building(bool[,] map, BuildingKind kind)
    {
        _map = map;
        _kind = kind;
    }

    public IEnumerable<Transform2D> SpawnBuildingCubes(Vector2 imageSize, Vector2 offset, float zLayer, float cubeScale)
    {
        var result = new List<Transform2D>();

        var widthRange = GetClearColumns();
        var mapWidht = widthRange.End.Value - widthRange.Start.Value;

        var heightRange = GetClearRows();
        var mapHeight = heightRange.End.Value - heightRange.Start.Value;

        for (var y = 0; y < mapHeight; y++)
        {
            for (var x = 0; x < mapWidht; x++)
            {
                if (!_map[y + heightRange.Start.Value, x + widthRange.Start.Value])
                {
                    continue;
                }

                var dx = mapWidht % 2 == 0 ? 0.5f : 0.0f;
                var dy = mapHeight % 2 == 0 ? 0.5f : 0.0f;

                var cubePos = new Vector2(dx, dy);

                var newTransform = new Transform2D()
                {
                    Translation = new Vector3(cubePos * imageSize + offset, zLayer),
                    Rotation = 0,
                    Scale = cubeScale,
                };

                result.Add(newTransform);
            }
        }

        return result;
    }

    private void GenerateMap()
    {
        var rnd = new Random();

        while (IsEmpty())
        {
            for (int i = 0; i < MapWidth; i++)
            {
                for (int j = 0; j < MapWidth; j++)
                {
                    _map[i, j] = rnd.NextDouble() == 0.5;
                }
            }
        }
    }

    public System.Range GetClearColumns()
    {
        int leftBorder = 0;
        int rightBorder = (int)MapWidth;

        for (uint i = 0; i < MapWidth; i++)
        {
            var isClear = true;
            for (uint j = 0; j < MapHeight; j++)
            {
                if (_map[j, i])
                {
                    isClear = false;
                    break;
                }
            }

            if (isClear)
            {
                leftBorder += 1;
            }
            else
            {
                break;
            }
        }

        for (uint i = MapWidth - 1; i > 0; i--)
        {
            var isClear = true;
            for (uint j = 0; j < MapHeight; j++)
            {
                if (_map[j, i])
                {
                    isClear = false;
                    break;
                }
            }

            if (isClear)
            {
                rightBorder -= 1;
            }
            else
            {
                break;
            }
        }

        return leftBorder..rightBorder;
    }

    public System.Range GetClearRows()
    {
        int topBorder = 0;
        int bottomBorder = (int)MapHeight;

        for (uint i = 0; i < MapHeight; i++)
        {
            var isClear = true;
            for (uint j = 0; j < MapWidth; j++)
            {
                if (_map[i, j])
                {
                    isClear = false;
                    break;
                }
            }

            if (isClear)
            {
                topBorder += 1;
            }
            else
            {
                break;
            }
        }

        for (uint i = MapHeight - 1; i > 0; i--)
        {
            var isClear = true;
            for (uint j = 0; j < MapWidth; j++)
            {
                if (_map[i, j])
                {
                    isClear = false;
                    break;
                }
            }

            if (isClear)
            {
                bottomBorder -= 1;
            }
            else
            {
                break;
            }
        }

        return topBorder..bottomBorder;
    }

    public bool IsEmpty()
    {
        for (int i = 0; i < MapWidth; i++)
        {
            for (int j = 0; j < MapWidth; j++)
            {
                if (_map[i, j])
                {
                    return false;
                }
            }
        }

        return true;
    }
}
