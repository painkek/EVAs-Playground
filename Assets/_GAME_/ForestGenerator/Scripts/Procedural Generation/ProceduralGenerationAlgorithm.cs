using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
public static class ProceduralGenerationAlgorithm
{

    // Hashset is a collection of unique elements
    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPosition, int walkLength)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();

        path.Add(startPosition);
        var previousPosition = startPosition;

        for (int i = 0; i < walkLength; i++)
        {                                           // will give us random direction
            var newPosition = previousPosition + Direction2D.GetRandomCardinalDirection();
            path.Add(newPosition);
            previousPosition = newPosition;
        }

        return path;
    }

    //  corridors
    public static List<Vector2Int> RandomWalkCorridor(Vector2Int startPosition, int corridorLength)
    {
        List<Vector2Int> corridor = new List<Vector2Int>();
        var direction = Direction2D.GetRandomCardinalDirection();
        var currentPosition = startPosition;
        corridor.Add(currentPosition);

        for (int i = 0; i < corridorLength; i++)
        {
            currentPosition += direction;
            corridor.Add(currentPosition);
        }
        return corridor;
    }

}

public static class Direction2D
{
    public static List<Vector2Int> cardinalDirectionsList = new List<Vector2Int>
    {
        new Vector2Int(0,1), //UP DIRECTION
        new Vector2Int(1,0), //RIGHT DIRECTION
        new Vector2Int(0,-1), //DOWN DIRECTION
        new Vector2Int(-1,0) //LEFT DIRECTION 
    };

    public static List<Vector2Int> diagonalDirectionsList = new List<Vector2Int>
    {
        new Vector2Int(1,1), //UP-RIGHT DIRECTION
        new Vector2Int(1,-1), //DOWN-RIGHT DIRECTION
        new Vector2Int(-1,-1), //DOWN-LEFT DIRECTION
        new Vector2Int(-1,1) //UP-LEFT DIRECTION
    };

    public static List<Vector2Int> eightDirectionList = new List<Vector2Int>
    {
        new Vector2Int(0,1), //UP DIRECTION
        new Vector2Int(1,1), //UP-RIGHT DIRECTION
        new Vector2Int(1,0), //RIGHT DIRECTION
        new Vector2Int(1,-1), //DOWN-RIGHT DIRECTION
        new Vector2Int(0,-1), //DOWN DIRECTION
        new Vector2Int(-1,-1), //DOWN-LEFT DIRECTION
        new Vector2Int(-1,0), //LEFT DIRECTION
        new Vector2Int(-1,1) //UP-LEFT DIRECTION
    };

    // generate randomDirection
    public static Vector2Int GetRandomCardinalDirection()
    {
        return cardinalDirectionsList[Random.Range(0, cardinalDirectionsList.Count)];
        // return eightDirectionList[Random.Range(0, eightDirectionList.Count)];

    }
}
