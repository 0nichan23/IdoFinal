using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RedPandaPassive", menuName = "Passives/RedPanda")]
public class RedPandaPassive : AnimalPassive
{
    //when dealing damage 75% chance to gain a crit hit buff and 25% to gain an attack speed debuff
    [SerializeField, Range(0, 10)] private float critHitCanceDuration;
    [SerializeField, Range(0, 10)] private float attackSpeedDebuffDuration;
    [SerializeField, Range(0, 1)] private float critHitChanceMod;
    [SerializeField, Range(0, 1)] private float attackSpeedDebuffMod;

    public override void SubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnHit.AddListener(ApplyStatus);
    }

    public override void UnSubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnHit.RemoveListener(ApplyStatus);
    }

    private void ApplyStatus(Damageable target, AnimalAttack attack, DamageDealer dealer, DamageHandler dmg)
    {
        if (Random.Range(0,100) >= 25)//crit
        {
            dealer.RefCharacter.Effectable.AddStatus(new CritHitBuff(critHitCanceDuration, critHitChanceMod), dealer);
        }
        else//attackspeed
        {
            dealer.RefCharacter.Effectable.AddStatus(new AttackSpeedDebuff(attackSpeedDebuffDuration, attackSpeedDebuffMod), dealer);
        }
    }
}
