using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapVisualizer : MonoBehaviour
{
    // Tilemap references
    [SerializeField] private Tilemap floorTilemap, wallTilemap, grassTilemap, sandTilemap, vegetationTilemap;

    // TileBase references
    [SerializeField] private TileBase floorTile, wallTop;
    [SerializeField] private TileBase[] vegetationTiles;

    // Rule Tiles
    [SerializeField] private RuleTile RfloorTile, RwallTile, RgrassTile, RsandTile;

    [SerializeField] private NPCManager npcManager;

    // Enum to choose between TileBase and RuleTile
    public enum TileType { Regular, Rule }
    public TileType tileType = TileType.Regular;

    // Probabilities for different tile placements
    [SerializeField] private float vegetationProbability = 0.3f;
    [SerializeField] private float _grassTileProbability = 0.4f;
    [SerializeField] private float _sandTileProbability = 0.4f;

    public float GrassTileProbability
    {
        get => _grassTileProbability;
        set => _grassTileProbability = Mathf.Clamp01(value);
    }

    public float SandTileProbability
    {
        get => _sandTileProbability;
        set => _sandTileProbability = Mathf.Clamp01(value);
    }

    public void PaintVegetation(IEnumerable<Vector2Int> floorPositions)
    {
        foreach (var position in floorPositions)
        {
            if (Random.value < vegetationProbability)
            {
                var randomVegetation = vegetationTiles[Random.Range(0, vegetationTiles.Length)];
                PaintSingleTile(vegetationTilemap, randomVegetation, position);
            }
        }
    }

    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
    {
        Clear(); // Clear existing tiles and NPCs

        if (tileType == TileType.Rule && RfloorTile != null)
        {
            Debug.Log("Using Rule Tile for Floor");
            PaintTiles(floorPositions, floorTilemap, RfloorTile);

            foreach (var position in floorPositions)
            {
                PaintSingleTile(floorTilemap, floorTile, position);

                if (Random.value < _grassTileProbability)
                {
                    PaintSingleTile(grassTilemap, RgrassTile, position);
                }
            }

            foreach (var position in floorPositions)
            {
                if (Random.value < _sandTileProbability)
                {
                    PaintSingleTile(sandTilemap, RsandTile, position);
                }
            }

            foreach (var position in floorPositions)
            {
                if (Random.value < vegetationProbability)
                {
                    var randomVegetation = vegetationTiles[Random.Range(0, vegetationTiles.Length)];
                    PaintSingleTile(vegetationTilemap, randomVegetation, position);
                }
            }


        }
        else
        {
            Debug.Log("Using Regular Tile for Floor");
        }
    }

    private Vector2Int GetRandomSpawnPoint(IEnumerable<Vector2Int> floorPositions)
    {
        return floorPositions.ElementAt(Random.Range(0, floorPositions.Count()));
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)
    {
        foreach (var position in positions)
        {
            PaintSingleTile(tilemap, tile, position);
        }
    }

    private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
    {
        var tilePosition = tilemap.WorldToCell((Vector3Int)position);
        tilemap.SetTile(tilePosition, tile);
    }

    public void PaintWallTiles(IEnumerable<Vector2Int> wallPositions)
    {
        if (tileType == TileType.Rule && RwallTile != null)
        {
            Debug.Log("Using Rule Tile for Walls");
            PaintTiles(wallPositions, wallTilemap, RwallTile);
        }
        else
        {
            Debug.Log("Using Regular Tile for Walls");
            PaintTiles(wallPositions, wallTilemap, wallTop);
        }
    }

    public void Clear()
    {
        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
        grassTilemap.ClearAllTiles();
        sandTilemap.ClearAllTiles();
        vegetationTilemap.ClearAllTiles();

        // // Clear existing NPCs
        // if (npcManager != null)
        // {
        //     npcManager.ClearAllNPCs();
        // }
    }

    public void PaintWallTiles(Vector2Int position)
    {
        if (tileType == TileType.Rule && RwallTile != null)
        {
            Debug.Log("Using Rule Tile for Walls");
            PaintSingleTile(wallTilemap, RwallTile, position);
        }
        else
            PaintSingleTile(wallTilemap, wallTop, position);
    }

    internal Tilemap GetFloorTilemap()
    {
        return floorTilemap;
    }
}
