using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuffaloPassive", menuName = "Passives/Buffalo")]
public class BuffaloPassive : AnimalPassive
{
    //deal more damage when bleeding 
    [SerializeField, Range(0,1)] private float damageMod;
    public override void SubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnHit.AddListener(DamageMod);
    }

    public override void UnSubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnHit.RemoveListener(DamageMod);
    }

    private void DamageMod(Damageable target, AnimalAttack attack, DamageDealer dealer, DamageHandler dmg)
    {
        foreach (var item in dealer.RefCharacter.Effectable.ActiveEffects)
        {
            if (item is Bleed)
            {
                dmg.AddMod(1 + damageMod);
                break;
            }
        }
    }
}
