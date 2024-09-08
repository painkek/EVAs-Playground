using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator
{
    public static void CreateWalls(HashSet<Vector2Int> floorPositions, TilemapVisualizer tilemapVisualizer)
    {
        var wallPositions = FindWallsInDirections(floorPositions, Direction2D.eightDirectionList);
        foreach (var position in wallPositions)
        {
            tilemapVisualizer.PaintWallTiles(position);
        }
    }

    private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorPositions, List<Vector2Int> directionList)
    {
        HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();
        foreach (var position in floorPositions)
        {
            foreach (var direction in directionList)
            {
                var neighbourPosition = position + direction;
                if (!floorPositions.Contains(neighbourPosition))
                {
                    wallPositions.Add(neighbourPosition);

                    // EXTEND WALLS
                    var extendedWallPosition = neighbourPosition + direction;
                    if (!floorPositions.Contains(extendedWallPosition))
                    {
                        wallPositions.Add(extendedWallPosition);
                    }

                }
            }
        }
        return wallPositions;
    }

    internal static object ExtendFloorTowardsWalls(HashSet<Vector2Int> floorPositions, List<Vector2Int> directionList, int additionalLayers = 1)
    {
        HashSet<Vector2Int> extendedFloorPositions = new HashSet<Vector2Int>(floorPositions);

        for (int i = 0; i <= additionalLayers; i++)
        {
            HashSet<Vector2Int> newPositions = new HashSet<Vector2Int>(extendedFloorPositions);
            foreach (var position in extendedFloorPositions)
            {
                foreach (var direction in directionList)
                {
                    var neighbourPosition = position + direction;
                    if (!floorPositions.Contains(neighbourPosition))
                    {
                        newPositions.Add(neighbourPosition);
                    }
                }
            }
            extendedFloorPositions = newPositions;
        }

        return extendedFloorPositions;
    }
}

