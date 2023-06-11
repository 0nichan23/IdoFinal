using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : CoroutineState
{
    public override bool IsLegal()
    {
        if (GameManager.Instance.LevelManager.CurrentLevel.GetNeighbours(handler.RefEnemy.Movement.CurrentTile).Contains(GameManager.Instance.PlayerWrapper.PlayerMovement.CurrentTile))
        {
            return true;
        }
        return false;
    }

    public override IEnumerator RunState()
    {
        handler.RefEnemy.Movement.LookTowardsMoveDirection(GameManager.Instance.PlayerWrapper.PlayerMovement.CurrentTile);
        handler.RefEnemy.AttackHandler.Attack();
        yield return new WaitForEndOfFrame();
    }

    public override void OnStateEnter()
    {
        //nothing on state enter here
    }

    public override void OnStateExit()
    {
        //nothing on state exit here
    }

}
