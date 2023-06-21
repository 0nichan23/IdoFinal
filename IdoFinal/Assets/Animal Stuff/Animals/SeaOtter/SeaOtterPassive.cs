using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SeaOtterPassive", menuName = "Passives/SeaOtter")]
public class SeaOtterPassive : AnimalPassive
{
    //chance to multihit a bleeding target
    [SerializeField, Range(0, 100)] private float multiHitChance;
    [SerializeField, Range(0, 10)] private int multiHits;
    public override void SubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnDealDamageFinal.AddListener(MultiHitBleedingTarget);
    }

    public override void UnSubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnDealDamageFinal.RemoveListener(MultiHitBleedingTarget);
    }

    private void MultiHitBleedingTarget(AnimalAttack attack, Damageable target, DamageDealer dealer, DamageHandler dmg)
    {
        if (Random.Range(0,100) > multiHitChance)
        {
            return;
        }
        foreach (var item in target.RefCharacter.Effectable.ActiveEffects)
        {
            if (item is Bleed)
            {
                for (int i = 0; i < multiHits; i++)
                {
                    target.GetMultiHit(dealer, dmg.CalcFinalDamageMult());
                }
                break;
            }
        }
        
    }
}
