using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class CorridorFirstForestGenerator : SimpleRandomWalkForestGenerator
{
    // PCG parameters
    [SerializeField] // show in inpsector
    private int corridorLength = 14, corridorCount = 5;
    [SerializeField] // show in inpsector
    [Range(0.1f, 1f)]
    private float roomPercent = 0.8f;

    [SerializeField] private NPCManager npcManager;

    protected override void RunProceduralGeneration()
    {
        CorridorFirstGeneration();
    }

    private void CorridorFirstGeneration()
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>(); // All positions (rooms + corridors)
        HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>();

        // Create corridors and gather corridor positions
        List<List<Vector2Int>> corridors = CreateCorridors(floorPositions, potentialRoomPositions);
        // Create rooms and gather room positions
        HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPositions);

        // Find dead-ends and create additional rooms at dead-ends
        List<Vector2Int> deadEnds = FindAllDeadEnds(floorPositions);
        CreateRoomsAtDeadEnd(deadEnds, roomPositions);

        // Add room positions to floor positions (to ensure rooms are part of the overall floor)
        floorPositions.UnionWith(roomPositions);

        // Process corridors
        for (int i = 0; i < corridors.Count; i++)
        {
            // Increase the size of corridors
            corridors[i] = IncreaseCorridorBrush3by3(corridors[i]);
            // Add corridor positions to the overall floor
            floorPositions.UnionWith(corridors[i]);
        }

        // Extend the floor tiles into the walls for visual enhancement
        HashSet<Vector2Int> extendedFloorPositions = AddExtraFloorTiles(floorPositions, Direction2D.cardinalDirectionsList);
        var addExtendedFloorPositions = WallGenerator.ExtendFloorTowardsWalls(floorPositions, Direction2D.eightDirectionList, 2);

        // Paint the floor and extended floor tiles
        tilemapVisualizer.PaintFloorTiles(floorPositions);
        tilemapVisualizer.PaintFloorTiles(extendedFloorPositions);

        // Generate walls around the floor
        WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);

        // Spawn NPCs only in the room positions (not in the corridors)
        if (npcManager != null)
        {
            npcManager.SpawnNPCsInRoom(roomPositions, tilemapVisualizer.GetFloorTilemap());
        }
    }

    // extended floor tiles
    private HashSet<Vector2Int> AddExtraFloorTiles(HashSet<Vector2Int> floorPositions, List<Vector2Int> cardinalDirectionsList)
    {
        HashSet<Vector2Int> extraFloorPositions = new HashSet<Vector2Int>();

        foreach (var position in floorPositions)
        {
            foreach (var direction in cardinalDirectionsList)
            {
                var neighbourPosition = position + direction;

                // If this position is not already part of the floor, add it as an extra floor tile
                if (!floorPositions.Contains(neighbourPosition))
                {
                    extraFloorPositions.Add(neighbourPosition);
                }
            }
        }

        // Return the union of the original floor positions and the extra floor positions
        return new HashSet<Vector2Int>(floorPositions.Union(extraFloorPositions));
    }

    // Increase corridor size by one
    public List<Vector2Int> IncreaseCorridorBrush3by3(List<Vector2Int> vector2Ints)
    {
        List<Vector2Int> newCorridor = new List<Vector2Int>();
        for (int i = 1; i < vector2Ints.Count; i++)
        {
            newCorridor.Add(vector2Ints[i]);
            for (int x = -1; x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {
                    newCorridor.Add(vector2Ints[i - 1] + new Vector2Int(x, y));
                }
            }
        }
        return newCorridor;
    }

    public List<Vector2Int> IncreaseCorridorSizeByOne(List<Vector2Int> corridor)
    {
        List<Vector2Int> newCorridor = new List<Vector2Int>();
        Vector2Int previousDirection = Vector2Int.zero;
        for (int i = 1; i < corridor.Count; i++)
        {
            Vector2Int directionFromCell = corridor[i] - corridor[i - 1]; // direction from cell
            if (previousDirection != Vector2Int.zero &&
                directionFromCell != previousDirection)
            {
                // handle corner
                for (int x = -1; x < 2; x++)
                {
                    for (int y = -1; y < 2; y++)
                    {
                        newCorridor.Add(corridor[i - 1] + new Vector2Int(x, y));
                    }
                }
                previousDirection = directionFromCell;
            }
            else
            {
                //Add a single cell in direction + 90 degrees
                Vector2Int newCorridorTileOffset
                 = GetDirection90From(directionFromCell);
                newCorridor.Add(corridor[i - 1]);
                newCorridor.Add(corridor[i - 1] + newCorridorTileOffset);
            }
        }
        return newCorridor;
    }

    private Vector2Int GetDirection90From(Vector2Int directionFromCell)
    {
        if (directionFromCell == Vector2Int.up)
            return Vector2Int.right;
        if (directionFromCell == Vector2Int.down)
            return Vector2Int.left;
        if (directionFromCell == Vector2Int.left)
            return Vector2Int.up;
        if (directionFromCell == Vector2Int.right)
            return Vector2Int.down;
        return Vector2Int.zero;
    }

    private void CreateRoomsAtDeadEnd(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomFloors)
    {
        foreach (var position in deadEnds)
        {
            if (roomFloors.Contains(position) == false)
            {
                var room = RunRandomWalk(randomWalkParameters, position);
                roomFloors.UnionWith(room);
            }
        }
    }

    private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> floorPositions)
    {
        List<Vector2Int> deadEnds = new List<Vector2Int>();
        foreach (var position in floorPositions)
        {
            int neighboursCount = 0;
            foreach (var direction in Direction2D.cardinalDirectionsList)
            {
                if (floorPositions.Contains(position + direction))
                    neighboursCount++;
            }
            if (neighboursCount == 1)
                deadEnds.Add(position);
        }
        return deadEnds;
    }

    // Generate rooms
    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPositions)
    {
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
        //number of rooms and calculated the count of potential rooms posiiton
        int roomToCreateCount = Mathf.RoundToInt(potentialRoomPositions.Count * roomPercent);
        //unique ID - x => Guid.NewGuid()
        List<Vector2Int> roomToCreate = potentialRoomPositions.OrderBy(x => Guid.NewGuid()).Take(roomToCreateCount).ToList();

        foreach (var roomsPosition in roomToCreate)
        {
            var roomFloor = RunRandomWalk(randomWalkParameters, roomsPosition);
            roomPositions.UnionWith(roomFloor);
        }
        return roomPositions;
    }

    // Generate corridors first
    private List<List<Vector2Int>> CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPositions)
    {
        var currentPosition = startPosition;
        potentialRoomPositions.Add(currentPosition); //start position
        List<List<Vector2Int>> corridors = new List<List<Vector2Int>>(); // expandable list

        for (int i = 0; i < corridorCount; i++)
        {
            var corridor = ProceduralGenerationAlgorithm.RandomWalkCorridor(currentPosition, corridorLength);
            corridors.Add(corridor);
            currentPosition = corridor[corridor.Count - 1];
            potentialRoomPositions.Add(currentPosition);
            floorPositions.UnionWith(corridor);
        }
        return corridors;
    }
}
