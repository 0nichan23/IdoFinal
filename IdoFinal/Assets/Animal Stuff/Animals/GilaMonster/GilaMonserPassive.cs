using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GilaMonsterPassive", menuName = "Passives/GilaMonster")]
public class GilaMonserPassive : AnimalPassive
{
    //true heal when attacking a poisoned enemy
    [SerializeField, Range(5,10)] private float amount;
    public override void SubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnHit.AddListener(TrueHealOnAttackingPoisonedEnemy);
    }

    public override void UnSubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnHit.RemoveListener(TrueHealOnAttackingPoisonedEnemy);
    }

    private void TrueHealOnAttackingPoisonedEnemy(Damageable target, AnimalAttack attack, DamageDealer dealer)
    {
        foreach (var item in target.RefCharacter.Effectable.ActiveEffects)
        {
            if (item is Poison)
            {
                dealer.RefCharacter.Damageable.HealTrueDamage(amount);            
            }
        }
    }
}
