using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;


public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Tilemap testTileMap;
    [SerializeField] private Grid testGrid;
    [SerializeField] private MyTile fillTile;
    [SerializeField] private int width;
    [SerializeField] private int length;
    [SerializeField] private float scale;
    [SerializeField] private int octaves;
    [SerializeField] private float persistence;
    [SerializeField] private float lacunarity;
    [SerializeField] private Vector2 offset;
    [SerializeField] private BiomeLayer biomeLayer;
    PerlinNoiseTest noise;

    [ContextMenu("test spawning prefab")]

    private void Start()
    {
        TestSpawn();
    }

    private void TestSpawn()
    {
        noise = new PerlinNoiseTest(width, length, scale, octaves, persistence, lacunarity, offset);
        float[,] heightMap = noise.Generate();
        for (int i = 0; i < heightMap.GetLength(0); i++)
        {
            for (int j = 0; j < heightMap.GetLength(1); j++)
            {
                testTileMap.SetTile(new Vector3Int(i, j, 0), biomeLayer.GetTileFromHeight(heightMap[i,j]));
            }
        }
        StartCoroutine(GameManager.Instance.LevemManager.CurrentLevel.StartUpLevel());
    }

}