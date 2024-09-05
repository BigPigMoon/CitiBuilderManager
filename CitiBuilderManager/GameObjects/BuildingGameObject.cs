using System;

namespace CitiBuilderManager.GameObjects;

public class BuildingGameObject
{
    private const uint MapWidth = 3;
    private const uint MapHeight = 3;

    public bool[,] Map { get; private set; } = new bool[MapWidth, MapWidth];
    private readonly BuildingKind _kind = new();

    public BuildingGameObject()
    {
        GenerateMap();
    }

    public BuildingGameObject(bool[,] map, BuildingKind kind)
    {
        Map = map;
        _kind = kind;
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
                    Map[i, j] = rnd.NextDouble() >= 0.5;
                }
            }
        }
    }

    public Range GetClearColumns()
    {
        int leftBorder = 0;
        int rightBorder = (int)MapWidth;

        for (uint i = 0; i < MapWidth; i++)
        {
            var isClear = true;
            for (uint j = 0; j < MapHeight; j++)
            {
                if (Map[j, i])
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
                if (Map[j, i])
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

    public Range GetClearRows()
    {
        int topBorder = 0;
        int bottomBorder = (int)MapHeight;

        for (uint i = 0; i < MapHeight; i++)
        {
            var isClear = true;
            for (uint j = 0; j < MapWidth; j++)
            {
                if (Map[i, j])
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
                if (Map[i, j])
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
                if (Map[i, j])
                {
                    return false;
                }
            }
        }

        return true;
    }
}
