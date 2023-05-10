using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackHandler : MonoBehaviour
{
    private Enemy refEnemy;

    private float attackSpeedMod;
    private float baseAttackSpeedMod;
    private float lastAttacked;
    private AttackTarget targeter = new AttackTarget();
    public void SetUp(Enemy givenEnemy)
    {
        refEnemy = givenEnemy;
        baseAttackSpeedMod = GetAttackSpeedModBase(refEnemy.RefAnimal.StatSheet.Speed);
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
        cd -= cd * (baseAttackSpeedMod + attackSpeedMod);
        return cd;
    }
    public void Attack()//attack the side the enemy is looking at
    {
        if (Time.time - lastAttacked >= GetAttackCoolDown())
        {
            targeter.AttackTiles(refEnemy, refEnemy.CurrentPos.GetPos, refEnemy.RefAnimal.Attack, refEnemy.DamageDealer);
            lastAttacked = Time.time;
        }
    }

}