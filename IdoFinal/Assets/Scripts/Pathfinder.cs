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
        FindPathToDest(test.CurrentPos, GameManager.Instance.PlayerWrapper.PlayerMovement.CurrentTile);
    }

    public void FindPathToDest(TileData startingPoint, TileData destenation)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();
        openList = new Heap<TileData>(GameManager.Instance.LevelManager.CurrentLevel.TraversableGround.Count);
        closedList = new List<TileData>();
        TileData currentTile;
        openList.Add(startingPoint);
        int imr = 0;
        while (openList.CurrentItemCount > 0)
        {
            imr++;
            if (imr == 100000)
            {
                return;
            }
            currentTile = openList.RemoveFirst();
            closedList.Add(currentTile);

            if (ReferenceEquals(currentTile, destenation))
            {
                //found path
                sw.Stop();
                UnityEngine.Debug.Log("found path in " + sw.Elapsed + " ms");
                RetracePath(startingPoint, destenation);
                return;
            }

            foreach (TileData neighbour in GameManager.Instance.LevelManager.CurrentLevel.GetNeighbours(currentTile))
            {
                if (closedList.Contains(neighbour))
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
    }


    private void RetracePath(TileData start, TileData end)
    {
        List<TileData> path = new List<TileData>();
        TileData cur = end;
        UnityEngine.Debug.Log("from " + start.GetPos);
        UnityEngine.Debug.Log("to " + end.GetPos);
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
        foreach (var item in path)
        {
            item.Overly.gameObject.SetActive(true);
            item.Overly.DamageColor();
        }
    }


    public int GetDistanceOfTiles(Vector3Int origin, Vector3Int destenation)
    {
        int distX = Mathf.Abs(destenation.x - origin.x);
        int distY = Mathf.Abs(destenation.y - origin.y);

        return distX + distY;
    }
}


