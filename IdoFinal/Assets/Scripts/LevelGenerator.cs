using UnityEngine;
using UnityEngine.Tilemaps;


public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Tilemap testTileMap;
    [SerializeField] private Grid testGrid;
    [SerializeField] private MyTile fillTile;
    [SerializeField] private int width;
    [SerializeField] private int length;
    [SerializeField] private int seed;
    [SerializeField] private float scale;
    [SerializeField] private int octaves;
    [SerializeField, Range(0, 1)] private float persistence;
    [SerializeField] private float lacunarity;
    [SerializeField] private Vector2 offset;
    [SerializeField] private BiomeLayer biomeLayer;

    [ContextMenu("test spawning prefab")]

    private void Start()
    {
        TestSpawn();
    }

    private void TestSpawn()
    {
        float[,] heightMap = Noise.GenerateNoiseMap(width, length, seed, scale, octaves, persistence, lacunarity, offset);
        for (int i = 0; i < heightMap.GetLength(0); i++)
        {
            for (int j = 0; j < heightMap.GetLength(1); j++)
            {
                testTileMap.SetTile(new Vector3Int(i, j, 0), biomeLayer.GetTileFromHeight(heightMap[i, j]));
            }
        }
        StartCoroutine(GameManager.Instance.LevemManager.CurrentLevel.StartUpLevel());
    }

    private void OnValidate()
    {
        if (width < 1)
        {
            width = 1;
        }
        if (length < 1)
        {
            length = 1;
        }
        if (lacunarity < 1)
        {
            lacunarity = 1;
        }
        if (octaves < 0)
        {
            octaves = 0;
        }

    }

}
