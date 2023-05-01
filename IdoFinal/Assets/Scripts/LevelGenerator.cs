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
    [SerializeField] private BiomeLayer biomeLayer;
    PerlinNoiseTest noise = new PerlinNoiseTest(15, 15, 5, 5, 5, 5, Vector2.one);
  
    [ContextMenu("test spawning prefab")]
    private void TestSpawn()
    {
        float[,] heightMap = noise.Generate();
        for (int i = 0; i < heightMap.GetLength(0); i++)
        {
            for (int j = 0; j < heightMap.GetLength(1); j++)
            {
                testTileMap.SetTile(new Vector3Int(i, j, 0), biomeLayer.GetTileFromHeight(heightMap[i,j]));
            }
        }
    }

}
