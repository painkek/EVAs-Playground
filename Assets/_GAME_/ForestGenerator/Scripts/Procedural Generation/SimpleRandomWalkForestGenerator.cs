using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Random = UnityEngine.Random;

public class SimpleRandomWalkForestGenerator : AbstractForestGenerator
{

    [SerializeField]
    protected SimpleRandomWalkSO randomWalkParameters;

    protected override void RunProceduralGeneration()
    {
        HashSet<Vector2Int> floorPositions = RunRandomWalk(randomWalkParameters, startPosition);
        // generate the initial floor positions
        // it gets the error when we try to extend the floor towards the walls
        //var initialFloorPositions = ProceduralGenerationAlgorithm.SimpleRandomWalk(startPosition, randomWalkParameters.walkLength);

        // extebd the floor towards the walls to elimnitae gaps
        // var extendedFloorPositions = WallGenerator.ExtendFloorTowardsWalls(initialFloorPositions, floorPositions, Direction2D.cardinalDirectionsList);
        // var extendedFloorPositions = WallGenerator.ExtendFloorTowardsWalls(initialFloorPositions, Direction2D.cardinalDirectionsList);


        // Assuming floorpositions is the orig set of floor tiles.
        HashSet<Vector2Int> extendedFloorPositions = AddExtraFloorTiles(floorPositions, Direction2D.cardinalDirectionsList);

        // add extend to wall
        var addExtendedFloorPositions = WallGenerator.ExtendFloorTowardsWalls(floorPositions, Direction2D.eightDirectionList, 2);

        tilemapVisualizer.Clear();
        //generate floor
        tilemapVisualizer.PaintFloorTiles(floorPositions);
        // extending floor to paint the tiles
        tilemapVisualizer.PaintFloorTiles(extendedFloorPositions);

        // tilemapVisualizer.PaintFloorTiles(extendedFloorPositions);

        //genereate walls
        WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);

        // WallGenerator.CreateWalls(extendedFloorPositions, tilemapVisualizer);

    }


    // add extra floor tiles
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

    protected HashSet<Vector2Int> RunRandomWalk(SimpleRandomWalkSO parameters, Vector2Int position)
    {
        var currentPosition = position;
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        for (int i = 0; i < randomWalkParameters.iterations; i++)
        {
            var path = ProceduralGenerationAlgorithm.SimpleRandomWalk(currentPosition, randomWalkParameters.walkLength);
            floorPositions.UnionWith(path); // method to allow us to unioun two hashsets
            if (randomWalkParameters.startRandomlyEachIteration)
                currentPosition = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
        }
        return floorPositions;
    }
}
