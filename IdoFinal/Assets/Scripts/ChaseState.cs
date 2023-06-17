using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : CoroutineState
{
    public override bool IsLegal()
    {
        if (GameManager.Instance.Pathfinder.GetDistanceOfTiles(handler.RefEnemy.Movement.CurrentTile.GetPos, GameManager.Instance.PlayerWrapper.PlayerMovement.CurrentTile.GetPos) <= handler.RefEnemy.DetectionRange)
        {
            return true;
        }
        return false;
    }

    public override void OnStateEnter()
    {
    }

    public override void OnStateExit()
    {
    }

    public override IEnumerator RunState()
    {
        TileData dest;
        if (ReferenceEquals(handler.RefEnemy.CurrentTileMap, GameManager.Instance.PlayerWrapper.CurrentTileMap))
        {
            dest = GameManager.Instance.PlayerWrapper.PlayerMovement.CurrentTile;
        }
        else
        {
            if (!ReferenceEquals(GameManager.Instance.LevelManager.CurrentLevel.GetTile(GameManager.Instance.PlayerWrapper.PlayerMovement.CurrentTile.GetPos, handler.RefEnemy.CurrentTileMap), null))
            {
                dest = GameManager.Instance.PlayerWrapper.PlayerMovement.CurrentTile;
            }
            else
            {
                List<TileData> foundNeighbors = GameManager.Instance.LevelManager.CurrentLevel.GetNeighbours(handler.RefEnemy.Movement.CurrentTile, handler.RefEnemy.CurrentTileMap);
                dest = foundNeighbors[Random.Range(0, foundNeighbors.Count)];
                yield break;
            }
        }
        if (!ReferenceEquals(dest, null) &&  handler.RefEnemy.CurrentTileMap.Contains(dest))
        {
            List<TileData> path = GameManager.Instance.Pathfinder.FindPathToDest(handler.RefEnemy.Movement.CurrentTile, dest, handler.RefEnemy.CurrentTileMap);
            yield return StartCoroutine(handler.RefEnemy.Movement.MoveEnemyTo(path[0]));
            yield return new WaitForEndOfFrame();
        }
    }
}
