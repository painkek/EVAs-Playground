// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.Tilemaps;

// public class TreeManager : MonoBehaviour
// {
//     public GameObject[] treePrefabs; // Array of tree prefabs
//     public float treeSpacing = 1.5f; // Minimum spacing between trees
//     public int numberOfTrees = 10; // Number of trees to place

//     private List<Vector3> placedTreePositions = new List<Vector3>();

//     public void PlaceTrees(IEnumerable<Vector2Int> floorPositions, Tilemap floorTilemap, Tilemap wallTilemap)
//     {
//         placedTreePositions.Clear();
//         foreach (var position in floorPositions)
//         {
//             Vector3 worldPosition = floorTilemap.CellToWorld((Vector3Int)position) + new Vector3(0.5f, 0.5f, 0f); // Center the tree on the tile

//             if (!IsTreeOnWall(wallTilemap, (Vector3Int)position) && !IsTreeTooClose(worldPosition))
//             {
//                 // Randomly select a tree prefab
//                 var randomTreePrefab = treePrefabs[Random.Range(0, treePrefabs.Length)];

//                 // Instantiate the selected tree
//                 Instantiate(randomTreePrefab, worldPosition, Quaternion.identity);
//                 placedTreePositions.Add(worldPosition);
//             }
//         }
//     }

//     private bool IsTreeOnWall(Tilemap wallTilemap, Vector3Int position)
//     {
//         return wallTilemap.HasTile(position);
//     }

//     private bool IsTreeTooClose(Vector3 position)
//     {
//         foreach (var treePosition in placedTreePositions)
//         {
//             if (Vector3.Distance(treePosition, position) < treeSpacing)
//             {
//                 return true;
//             }
//         }
//         return false;
//     }

//     public void ClearTrees()
//     {
//         // Destroy all child objects (trees) of the TreeManager GameObject
//         foreach (Transform child in transform)
//         {
//             Destroy(gameObject);
//         }
//         placedTreePositions.Clear(); // Clear the list of tree positions
//     }

// }
