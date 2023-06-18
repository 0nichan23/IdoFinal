using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DragonPassive", menuName = "Passives/Dragon")]
public class DragonPassive : AnimalPassive
{
    //all attacks have a chance to burn. Deal more damage to burned targets
    [SerializeField, Range(0, 10)] private float duration;
    [SerializeField, Range(0, 1)] private float burnDamage;
    [SerializeField, Range(0, 1)] private float damageMod;
    [SerializeField, Range(0, 100)] private float burnChance;
    public override void SubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnHit.AddListener(ApplyBurn);
    }

    public override void UnSubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnHit.RemoveListener(ApplyBurn);
    }

    private void ApplyBurn(Damageable target, AnimalAttack attack, DamageDealer dealer, DamageHandler dmg)
    {
        if (Random.Range(0,100) <= burnChance)
        {
            target.RefCharacter.Effectable.AddStatus(new Burn(duration, burnDamage), dealer);
        }

        foreach (var item in target.RefCharacter.Effectable.ActiveEffects)
        {
            if (item is Burn)
            {
                dmg.AddMod(1 + damageMod);
            }
        }

    }
}
