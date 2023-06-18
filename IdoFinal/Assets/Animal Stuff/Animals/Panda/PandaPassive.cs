using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PandaPassive", menuName = "Passives/Panda")]
public class PandaPassive : AnimalPassive
{
    //the less accurate the attack (down to a min) is , the more damage it does (up to a max)
    [SerializeField, Range(0, 1)] private float damageMax;
    [SerializeField, Range(0, 1)] private float minAccuracy;
    public override void SubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnHit.AddListener(IncreaseDamage);
    }

    public override void UnSubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnHit.RemoveListener(IncreaseDamage);
    }

    private void IncreaseDamage(Damageable target, AnimalAttack attack, DamageDealer dealer, DamageHandler dmg)
    {
        float dmgMod = damageMax;
        float acc = dealer.HitChance - minAccuracy;
        //max damage = 0.5/ hit chance 1.2/ mid accuract 0.6 => 1.2 - 0.6 = 0.6 => 0.5mod -= 0.5 * 0.6
        dmgMod -= dmgMod * acc;
        dmg.AddMod(1 + dmgMod);
    }
}
