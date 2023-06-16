using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WyvernPassive", menuName = "Passives/Wyvern")]
public class WyvernPassive : AnimalPassive
{
    //attacks more powerful with infused electricity and have a chance to stun
    [SerializeField, Range(0, 1)] private float damageIncrease;
    [SerializeField, Range(0, 10)] private float stunDuration;
    [SerializeField, Range(0, 100)] private float stunChance;
    public override void SubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnHit.AddListener(DamageIncreaseAndStunChance);
    }

    public override void UnSubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnHit.RemoveListener(DamageIncreaseAndStunChance);
    }

    private void DamageIncreaseAndStunChance(Damageable target, AnimalAttack attack, DamageDealer dealer)
    {
        attack.Damage.AddMod(1 + damageIncrease);
        if (Random.Range(0,100) <= stunChance)
        {
            target.RefCharacter.Effectable.AddStatus(new Stun(stunDuration), dealer);
        }
    }
}
