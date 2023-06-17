using UnityEngine;
using UnityEngine.Events;

public class EnemyAttackHandler : MonoBehaviour
{
    private Enemy refEnemy;

    private float attackSpeedMod;
    private float baseAttackSpeedMod;
    private float lastAttacked;
    private float lastSecondAttacked;
    private bool charging;
    private bool shooting;

    private AttackTarget targeter = new AttackTarget();
    private AttackCounter attackCounter = new AttackCounter();

    public UnityEvent OnAttackPerformed;
    public AttackCounter AttackCounter { get => attackCounter; }
    public float AttackSpeed { get => Mathf.Clamp(baseAttackSpeedMod + attackSpeedMod, 0f, 0.9f); }
    public bool Charging { get => charging; set => charging = value; }
    public bool Shooting { get => shooting; set => shooting = value; }

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
    public float GetSecondAttackCoolDown()
    {
        float cd = refEnemy.RefAnimal.SecondAttack.CoolDown;
        cd -= cd * AttackSpeed;
        return cd;
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
            targeter.AttackTiles(refEnemy.LookingTowards, refEnemy.Movement.CurrentTile.GetPos, refEnemy.RefAnimal.Attack, refEnemy);
            OnAttackPerformed?.Invoke();
            lastAttacked = Time.time;
        }
    }

    public void SecondAttack()
    {
        if (ReferenceEquals(refEnemy.RefAnimal.SecondAttack, null))
        {
            return;
        }
        if (Time.time - lastSecondAttacked >= GetSecondAttackCoolDown())
        {
            refEnemy.Anim.AttackAnim();
            if (refEnemy.RefAnimal.SecondAttack.Charge)
            {
                refEnemy.Charge(refEnemy.RefAnimal.SecondAttack);
            }
            else if(refEnemy.RefAnimal.SecondAttack.Projectile)
            {
                refEnemy.FireProjectile(refEnemy.RefAnimal.SecondAttack);
            }
            OnAttackPerformed?.Invoke();
            lastSecondAttacked = Time.time;
        }
    }

}
