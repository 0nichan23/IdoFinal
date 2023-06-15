using UnityEngine;
using UnityEngine.Events;

public class EnemyAttackHandler : MonoBehaviour
{
    private Enemy refEnemy;

    private float attackSpeedMod;
    private float baseAttackSpeedMod;
    private float lastAttacked;
    private AttackTarget targeter = new AttackTarget();
    private AttackCounter attackCounter = new AttackCounter();

    public UnityEvent OnAttackPerformed;
    public AttackCounter AttackCounter { get => attackCounter; }
    public float AttackSpeed { get => Mathf.Clamp(baseAttackSpeedMod + attackSpeedMod, 0f, 0.9f); }

    public void SetUp(Enemy givenEnemy)
    {
        refEnemy = givenEnemy;
        baseAttackSpeedMod = GetAttackSpeedModBase(refEnemy.RefAnimal.StatSheet.Speed);
    }
    private void Start()
    {
        OnAttackPerformed.AddListener(attackCounter.CountAttacks);
    }
    private float GetAttackSpeedModBase(int baseSpeed)
    {
        //10 speed = 100% faster attack speed/ half cool down
        float speedMod = 0f;
        for (int i = 0; i < baseSpeed; i++)
        {
            speedMod += 0.05f;
        }
        return speedMod;
    }

    public void AddAttackSpeed(float givenMod)
    {
        attackSpeedMod += givenMod;
    }
    public float GetAttackCoolDown()
    {
        float cd = refEnemy.RefAnimal.Attack.CoolDown;
        cd -= cd * AttackSpeed;
        return cd;
    }
    public void Attack()//attack the side the enemy is looking at
    {
        if (Time.time - lastAttacked >= GetAttackCoolDown())
        {
            refEnemy.Anim.AttackAnim();
            targeter.AttackTiles(refEnemy.LookingTowards, refEnemy.Movement.CurrentTile.GetPos, refEnemy.RefAnimal.Attack, refEnemy.DamageDealer);
            OnAttackPerformed?.Invoke();
            lastAttacked = Time.time;
        }
    }

}
