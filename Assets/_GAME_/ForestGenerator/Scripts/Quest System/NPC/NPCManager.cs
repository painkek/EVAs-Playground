using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NPCManager : MonoBehaviour
{
    [SerializeField] private GameObject questGiverPrefab;  // Quest Giver NPC prefab
    [SerializeField] private GameObject helperPrefab;      // Helper NPC prefab
    [SerializeField] private GameObject greeterPrefab;     // Greeter NPC prefab

    [SerializeField] private int questGiverCount = 1;      // Number of Quest Giver NPCs to spawn
    [SerializeField] private int helperCount = 1;          // Number of Helper NPCs to spawn
    [SerializeField] private int greeterCount = 1;         // Number of Greeter NPCs to spawn

    private List<GameObject> currentNPCs = new List<GameObject>(); // Track spawned NPCs

    public void SpawnNPCsInRoom(HashSet<Vector2Int> floorPositions, Tilemap floorTilemap)
    {
        ClearAllNPCs();  // Clear any existing NPCs

        List<Vector2Int> availablePositions = floorPositions.ToList();
        int totalNPCsToSpawn = questGiverCount + helperCount + greeterCount; // Total number of NPCs to spawn

        // Ensure we don't try to spawn more NPCs than available positions
        totalNPCsToSpawn = Mathf.Min(totalNPCsToSpawn, availablePositions.Count);

        for (int i = 0; i < totalNPCsToSpawn; i++)
        {
            // Select a random spawn point from the available floor positions
            Vector2Int spawnPoint = availablePositions[UnityEngine.Random.Range(0, availablePositions.Count)];
            Vector3 worldPosition = floorTilemap.CellToWorld((Vector3Int)spawnPoint) + new Vector3(0.5f, 0.5f, 0f);

            GameObject npcToSpawn;

            // Spawn based on NPC type, in order (first quest givers, then helpers, then greeters)
            if (i < questGiverCount)
            {
                npcToSpawn = Instantiate(questGiverPrefab, worldPosition, Quaternion.identity);  // Spawn Quest Giver
            }
            else if (i < questGiverCount + helperCount)
            {
                npcToSpawn = Instantiate(helperPrefab, worldPosition, Quaternion.identity);      // Spawn Helper NPC
            }
            else
            {
                npcToSpawn = Instantiate(greeterPrefab, worldPosition, Quaternion.identity);     // Spawn Greeter NPC
            }

            currentNPCs.Add(npcToSpawn);
            availablePositions.Remove(spawnPoint);  // Remove used position
        }
    }

    public void ClearAllNPCs()
    {
        foreach (var npc in currentNPCs)
        {
            if (npc != null)
            {
                DestroyImmediate(npc);
            }
        }
        currentNPCs.Clear();  // Clear the list of NPCs
    }
}




// using System;
// using System.Collections.Generic;
// using System.Linq;
// using UnityEngine;
// using UnityEngine.Tilemaps;


// public class NPCManager : MonoBehaviour
// {



//     [SerializeField] private GameObject[] questGiverPrefabs;  // Different Quest Giver NPC prefabs
//     [SerializeField] private GameObject[] helperPrefabs;      // Different Helper NPC prefabs
//     [SerializeField] private GameObject[] greeterPrefabs;     // Different Greeter NPC prefabs

//     [SerializeField] private int questGiverCount = 1;         // Number of Quest Giver NPCs to spawn
//     [SerializeField] private int helperCount = 1;             // Number of Helper NPCs to spawn
//     [SerializeField] private int greeterCount = 1;            // Number of Greeter NPCs to spawn

//     private List<GameObject> currentNPCs = new List<GameObject>(); // Track spawned NPCs

//     public void SpawnQuestGiver(Vector3 position)
//     {
//         int randomIndex = UnityEngine.Random.Range(0, questGiverPrefabs.Length);
//         GameObject questGiver = Instantiate(questGiverPrefabs[randomIndex], position, Quaternion.identity);
//         currentNPCs.Add(questGiver);
//     }

//     public void SpawnHelper(Vector3 position)
//     {
//         int randomIndex = UnityEngine.Random.Range(0, helperPrefabs.Length);
//         GameObject helperNPC = Instantiate(helperPrefabs[randomIndex], position, Quaternion.identity);
//         currentNPCs.Add(helperNPC);
//     }

//     public void SpawnGreeter(Vector3 position)
//     {
//         int randomIndex = UnityEngine.Random.Range(0, greeterPrefabs.Length);
//         GameObject greeterNPC = Instantiate(greeterPrefabs[randomIndex], position, Quaternion.identity);
//         currentNPCs.Add(greeterNPC);
//     }

//     public void SpawnNPCsInRoom(HashSet<Vector2Int> floorPositions, Tilemap floorTilemap)
//     {
//         ClearAllNPCs();  // Clear any existing NPCs

//         List<Vector2Int> availablePositions = floorPositions.ToList();
//         int totalNPCsToSpawn = questGiverCount + helperCount + greeterCount;

//         totalNPCsToSpawn = Mathf.Min(totalNPCsToSpawn, availablePositions.Count);

//         for (int i = 0; i < totalNPCsToSpawn; i++)
//         {
//             Vector2Int spawnPoint = availablePositions[UnityEngine.Random.Range(0, availablePositions.Count)];
//             Vector3 worldPosition = floorTilemap.CellToWorld((Vector3Int)spawnPoint) + new Vector3(0.5f, 0.5f, 0f);

//             if (i < questGiverCount)
//             {
//                 SpawnQuestGiver(worldPosition);
//             }
//             else if (i < questGiverCount + helperCount)
//             {
//                 SpawnHelper(worldPosition);
//             }
//             else
//             {
//                 SpawnGreeter(worldPosition);
//             }

//             availablePositions.Remove(spawnPoint);  // Remove used position
//         }
//     }

//     public void ClearAllNPCs()
//     {
//         foreach (var npc in currentNPCs)
//         {
//             if (npc != null)
//             {
//                 DestroyImmediate(npc);
//             }
//         }
//         currentNPCs.Clear();
//     }
// }


// // Compare this snippet from Assets/_GAME_/ForestGenerator/Scripts/Procedural%20Generation/RoomFirstForestGeneration.cs:
// // public class NPCManager : MonoBehaviour
// // {
// //     [SerializeField] private GameObject questGiverPrefab;  // Quest Giver NPC prefab
// //     [SerializeField] private GameObject helperPrefab;      // Helper NPC prefab
// //     [SerializeField] private GameObject greeterPrefab;     // Greeter NPC prefab

// //     [SerializeField] private int questGiverCount = 1;      // Number of Quest Giver NPCs to spawn
// //     [SerializeField] private int helperCount = 1;          // Number of Helper NPCs to spawn
// //     [SerializeField] private int greeterCount = 1;         // Number of Greeter NPCs to spawn

// //     private List<GameObject> currentNPCs = new List<GameObject>(); // Track spawned NPCs

// //     public void SpawnQuestGiver(Vector3 position)
// //     {
// //         GameObject questGiver = Instantiate(questGiverPrefab, position, Quaternion.identity);
// //         currentNPCs.Add(questGiver);
// //     }

// //     public void SpawnHelper(Vector3 position)
// //     {
// //         GameObject helperNPC = Instantiate(helperPrefab, position, Quaternion.identity);
// //         currentNPCs.Add(helperNPC);
// //     }

// //     public void SpawnGreeter(Vector3 position)
// //     {
// //         GameObject greeterNPC = Instantiate(greeterPrefab, position, Quaternion.identity);
// //         currentNPCs.Add(greeterNPC);
// //     }


// //     public void SpawnNPCsInRoom(HashSet<Vector2Int> floorPositions, Tilemap floorTilemap)
// //     {
// //         ClearAllNPCs();  // Clear any existing NPCs

// //         List<Vector2Int> availablePositions = floorPositions.ToList();
// //         int totalNPCsToSpawn = questGiverCount + helperCount; // Total number of NPCs to spawn

// //         // Ensure we don't try to spawn more NPCs than available positions
// //         totalNPCsToSpawn = Mathf.Min(totalNPCsToSpawn, availablePositions.Count);

// //         for (int i = 0; i < totalNPCsToSpawn; i++)
// //         {
// //             // Select a random spawn point from the available floor positions
// //             Vector2Int spawnPoint = availablePositions[UnityEngine.Random.Range(0, availablePositions.Count)];
// //             Vector3 worldPosition = floorTilemap.CellToWorld((Vector3Int)spawnPoint) + new Vector3(0.5f, 0.5f, 0f);

// //             // Spawn Quest Givers first, then Helpers
// //             if (i < questGiverCount)
// //             {
// //                 SpawnQuestGiver(worldPosition);  // Spawn Quest Giver
// //             }
// //             else
// //             {
// //                 SpawnHelper(worldPosition);      // Spawn Helper NPC
// //             }

// //             availablePositions.Remove(spawnPoint);  // Remove used position
// //         }
// //     }

// //     public void ClearAllNPCs()
// //     {
// //         foreach (var npc in currentNPCs)
// //         {
// //             if (npc != null)
// //             {
// //                 DestroyImmediate(npc);
// //             }
// //         }
// //         currentNPCs.Clear();  // Clear the list of NPCs
// //     }
// // }
