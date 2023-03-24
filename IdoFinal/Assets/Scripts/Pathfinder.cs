using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    List<TileData> openList;
    List<TileData> closedList;

    [SerializeField] private Enemy test;

    List<TileData> path;

    private void Update()
    {
        FindPathToDest(test.CurrentPos, GameManager.Instance.PlayerWrapper.PlayerMovement.CurrentTile);
    }



    public void FindPathToDest(TileData startingPoint, TileData destenation)
    {
        openList = new List<TileData>();
        closedList = new List<TileData>();
        TileData currentTile;
        openList.Add(startingPoint);

        int imr =0;
        while (openList.Count > 0)
        {
            imr++;
            if (imr == 100000)
            {
                Debug.LogError("weewoo");
                return;
            }
            currentTile = openList[0];
            foreach (TileData openTile in openList)
            {
                if (openTile.totalCost < currentTile.totalCost || (openTile.totalCost == currentTile.totalCost && openTile.totalCost < currentTile.totalCost))
                {
                    currentTile = openTile;
                }
            }

            openList.Remove(currentTile);
            closedList.Add(currentTile);

            if (ReferenceEquals(currentTile, destenation))
            {
                //retract and walk along path
                RetracePath(startingPoint, destenation);
                return;
            }

            foreach (TileData neighbour in GameManager.Instance.LevemManager.CurrentLevel.GetNeighbours(currentTile))
            {
                int newNeighbourMovementCost = currentTile.costToStart + GetDistanceOfTiles(currentTile.GetPos, neighbour.GetPos);
                if (newNeighbourMovementCost < neighbour.costToStart || !openList.Contains(neighbour))
                {
                    neighbour.costToStart = newNeighbourMovementCost;
                    neighbour.costToEnd = GetDistanceOfTiles(currentTile.GetPos, destenation.GetPos);
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
        while(!ReferenceEquals(cur, start))
        {
            path.Add(cur);
            cur = cur.PathParent;
        }
        path.Reverse();
        this.path = path;
    }


    public int GetDistanceOfTiles(Vector3Int origin, Vector3Int destenation)
    {
        int distX = Mathf.Abs(destenation.x - origin.x);
        int distY = Mathf.Abs(destenation.y - origin.y);

        return distX + distY;
    }

    private void OnDrawGizmosSelected()
    {
        foreach (var item in path)
        {
            Gizmos.color = Color.black;
            Gizmos.DrawSphere(item.GetStandingPos, 1);
        }
    }
}


