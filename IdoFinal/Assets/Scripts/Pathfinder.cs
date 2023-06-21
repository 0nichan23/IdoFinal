using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    Heap<TileData> openList;
    List<TileData> closedList;

    [SerializeField] private Enemy test;

    List<TileData> path = new List<TileData>();

    [ContextMenu("find path")]
    public void AttempFindingPath()
    {
        //FindPathToDest(test.CurrentPos, GameManager.Instance.PlayerWrapper.PlayerMovement.CurrentTile);
    }

    public List<TileData> FindPathToDest(TileData startingPoint, TileData destenation, Dictionary<Vector3Int, TileData> map)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();
        openList = new Heap<TileData>(map.Count);
        closedList = new List<TileData>();
        TileData currentTile;
        openList.Add(startingPoint);
        int imr = 0;
        while (openList.CurrentItemCount > 0)
        {
            imr++;
            if (imr == 100000)
            {
                return null;
            }
            currentTile = openList.RemoveFirst();
            closedList.Add(currentTile);

            if (ReferenceEquals(currentTile, destenation))
            {
                //found path
                sw.Stop();
                return RetracePath(startingPoint, destenation);
            }

            foreach (TileData neighbour in GameManager.Instance.LevelManager.CurrentLevel.GetNeighbours(currentTile, map))
            {
                if (closedList.Contains(neighbour) || (neighbour.Occupied && !ReferenceEquals(neighbour, destenation)))
                {
                    //if the neighbour was already the current tile.
                    continue;
                }

                int newNeighbourMovementCost = currentTile.costToStart + GetDistanceOfTiles(currentTile.GetPos, neighbour.GetPos);
                if (newNeighbourMovementCost < neighbour.costToStart || !openList.Contains(neighbour))
                {
                    neighbour.costToStart = newNeighbourMovementCost;
                    neighbour.costToEnd = GetDistanceOfTiles(neighbour.GetPos, destenation.GetPos);
                    neighbour.PathParent = currentTile;
                    if (!openList.Contains(neighbour))
                    {
                        openList.Add(neighbour);
                    }
                }
            }

        }
        return null;
    }


    private List<TileData> RetracePath(TileData start, TileData end)
    {
        List<TileData> path = new List<TileData>();
        TileData cur = end;
        while (!ReferenceEquals(cur, start))
        {
            if (cur.GetPos == start.GetPos)
            {
                break;
            }
            path.Add(cur);
            cur = cur.PathParent;
        }
        path.Reverse();
        return path;
    }


    public int GetDistanceOfTiles(Vector3Int origin, Vector3Int destenation)
    {
        int distX = Mathf.Abs(destenation.x - origin.x);
        int distY = Mathf.Abs(destenation.z - origin.z);

        return distX + distY;
    }
}


