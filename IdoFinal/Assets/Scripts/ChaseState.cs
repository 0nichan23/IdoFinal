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
        //set the destenation to the player tile, only move to it if the enemy map contains it?
        TileData dest;
        dest = GameManager.Instance.PlayerWrapper.PlayerMovement.CurrentTile;
        //if the player and the enemy are on the same plane 
        if (/*handler.RefEnemy.MovementMode == GameManager.Instance.PlayerWrapper.MovementMode*/ handler.RefEnemy.CurrentTileMap.Contains(GameManager.Instance.PlayerWrapper.CurrentTile)) //must be on the same plane because of the pathfinder
        {
            List<TileData> path = GameManager.Instance.Pathfinder.FindPathToDest(handler.RefEnemy.Movement.CurrentTile, dest, handler.RefEnemy.CurrentTileMap);
            if (!ReferenceEquals(path, null) &&  handler.RefEnemy.CurrentTileMap.Contains(path[0]))
            {
                yield return StartCoroutine(handler.RefEnemy.Movement.MoveEnemyTo(path[0]));
            }
        }
        //if the player and enemy are on different planes 
        else
        {
            List<TileData> path = GameManager.Instance.Pathfinder.FindPathToDest(handler.RefEnemy.Movement.CurrentTile, dest, GameManager.Instance.LevelManager.CurrentLevel.TotalMap);
            if (handler.RefEnemy.CurrentTileMap.Contains(path[0]))
            {
                yield return StartCoroutine(handler.RefEnemy.Movement.MoveEnemyTo(path[0]));
            }
        }
        yield return new WaitForEndOfFrame();
    }
}
