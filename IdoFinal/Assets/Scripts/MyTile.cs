using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "newTile", menuName = "Tile")]

public class MyTile : TileBase
{
    [SerializeField] private GameObject go;

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref UnityEngine.Tilemaps.TileData tileData)
    {
        tileData.gameObject = go;
    }

}
