using System.Collections;
using UnityEngine;

public class SecondaryAttackState : CoroutineState
{
    [SerializeField] private int maxDistacne;
    public override bool IsLegal()
    {
        if (ReferenceEquals(handler.RefEnemy.RefAnimal.SecondAttack, null))
        {
            return false;
        }

        float distance = GameManager.Instance.Pathfinder.GetDistanceOfTiles(handler.RefEnemy.Movement.CurrentTile.GetPos, GameManager.Instance.PlayerWrapper.PlayerMovement.CurrentTile.GetPos);
        if (distance <= maxDistacne && distance > 2)
        {
            if (GameManager.Instance.LevelManager.CurrentLevel.CheckStraightLineX(handler.RefEnemy.Movement.CurrentTile, GameManager.Instance.PlayerWrapper.PlayerMovement.CurrentTile, handler.RefEnemy.CurrentTileMap) ||
                GameManager.Instance.LevelManager.CurrentLevel.CheckStraightLineZ(handler.RefEnemy.Movement.CurrentTile, GameManager.Instance.PlayerWrapper.PlayerMovement.CurrentTile, handler.RefEnemy.CurrentTileMap))
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
        handler.RefEnemy.Movement.LookTowardsMoveDirection(GameManager.Instance.PlayerWrapper.PlayerMovement.CurrentTile);
        handler.RefEnemy.AttackHandler.SecondAttack();
        if (handler.RefEnemy.RefAnimal.SecondAttack is ProjectileAttack)
        {
            yield return new WaitForSeconds(1);
        }
        else if(handler.RefEnemy.RefAnimal.SecondAttack is Charge)
        {
            yield return new WaitUntil(() => !handler.RefEnemy.AttackHandler.Charging);
        }
    }
}
