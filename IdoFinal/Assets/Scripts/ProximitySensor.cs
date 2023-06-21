using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximitySensor
{
    public List<TileData> GetTilesInRadius(int radius, TileData origin, Dictionary<Vector3Int, TileData> map)
    {
        List<TileData> validTiles = new List<TileData>();
        for (int i = -radius; i < radius; i++)
        {
            for (int j = -radius; j < radius; j++)
            {
                TileData tile = GameManager.Instance.LevelManager.CurrentLevel.GetTile(origin.GetPos + new Vector3Int(i, 0, j), map);
                if (!ReferenceEquals(tile, null))
                {
                    validTiles.Add(tile);
                }
            }
        }
        return validTiles;
    }
}
