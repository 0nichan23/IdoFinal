using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlaneState : CoroutineState
{
    //enemy will change plane if the player is within sight, and on another plane and if one of the neighbouring tiles if of that same plane
    [SerializeField] private float detectionZone;
    public override bool IsLegal()
    {
        if (handler.RefEnemy.MovementMode == GameManager.Instance.PlayerWrapper.MovementMode)
        {
            return false;
        }
        else if (GameManager.Instance.Pathfinder.GetDistanceOfTiles(handler.RefEnemy.Movement.CurrentTile.GetPos, GameManager.Instance.PlayerWrapper.PlayerMovement.CurrentTile.GetPos) <= detectionZone)
        {
            List<TileData> neighboursInPlane = GameManager.Instance.LevelManager.CurrentLevel.GetNeighbours(handler.RefEnemy.Movement.CurrentTile, GameManager.Instance.PlayerWrapper.CurrentTileMap);
            if (neighboursInPlane.Count > 0)
            {
                return true;
            }
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
        switch (GameManager.Instance.PlayerWrapper.MovementMode)
        {
            case MovementMode.Ground:
                handler.RefEnemy.SetWalkMode();
                break;
            case MovementMode.Water:
                handler.RefEnemy.SetSwimMode();
                break;
            case MovementMode.Air:
                handler.RefEnemy.SetFlightMode();
                break;
        }
        yield return new WaitForEndOfFrame();
    }
}
