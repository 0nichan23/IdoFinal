using System;
using System.Collections.Generic;
using UnityEngine;


public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int length;
    [SerializeField] private float scale;
    [SerializeField] private int octaves;
    [SerializeField, Range(0, 1)] private float persistence;
    [SerializeField] private float lacunarity;
    [SerializeField] private Vector2 offset;
    [SerializeField] private List<BiomeData> biomeData = new List<BiomeData>();
    [SerializeField] private Level levelPrefab;

    public List<BiomeData> BiomeData { get => biomeData;}

    [ContextMenu("test spawning prefab")]
    public Level CreateLevel()
    {
        Level newLevel = Instantiate(levelPrefab, transform);
        Filllevel(newLevel, biomeData[UnityEngine.Random.Range(0, biomeData.Count)]);
        return newLevel;
    }

    private void Filllevel(Level givenLevel, BiomeData biome)
    {
        givenLevel.Habitat = biome.BiomeType;
        givenLevel.EnemyCreator = biome.HeightData.EnemyCreator;
        int seed = UnityEngine.Random.Range(0, 100000);
        float[,] heightMap = Noise.GenerateNoiseMap(width, length, seed, scale, octaves, persistence, lacunarity, offset);
        for (int i = 0; i < heightMap.GetLength(0); i++)
        {
            for (int j = 0; j < heightMap.GetLength(1); j++)
            {
                givenLevel.Tilemap.SetTile(new Vector3Int(i, j, 0), biome.HeightData.GetTileFromHeight(heightMap[i, j]));
            }
        }

        //encasing the level
        for (int x = -1; x < heightMap.GetLength(0); x++)
        {
            givenLevel.Tilemap.SetTile(new Vector3Int(x, 0, 0), biome.HeightData.EncasingBlock);
        }
        for (int x = -1; x < heightMap.GetLength(0); x++)
        {
            givenLevel.Tilemap.SetTile(new Vector3Int(x, heightMap.GetLength(1), 0), biome.HeightData.EncasingBlock);
        }
        for (int y = 0; y < heightMap.GetLength(1); y++)
        {
            givenLevel.Tilemap.SetTile(new Vector3Int(-1, y, 0), biome.HeightData.EncasingBlock);
        }
        for (int y = 0; y < heightMap.GetLength(1)+1; y++)
        {
            givenLevel.Tilemap.SetTile(new Vector3Int(heightMap.GetLength(0), y, 0), biome.HeightData.EncasingBlock);
        }
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

    public BiomeData GetBiomeDataFromHabitat(Habitat habitat)
    {
        foreach (var item in biomeData)
        {
            if (item.BiomeType == habitat)
            {
                return item;
            }
        }
        return null;
    }

}

[System.Serializable]
public class BiomeData
{
    public BiomeLayer HeightData;
    public Habitat BiomeType;
}
