using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "HyenaPassive", menuName = "Passives/Hyena")]
public class HyenaPassive : AnimalPassive
{
    //critical strikes have a chance to stun
    [SerializeField, Range(0,2f)] private float stunDutration;
    [SerializeField, Range(1, 100)] private float chance;
    public override void SubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnDealCritDamage.AddListener(StunOnCrit);
    }

    public override void UnSubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnDealCritDamage.RemoveListener(StunOnCrit);
    }

    private void StunOnCrit(AnimalAttack attack, Damageable target, DamageDealer dealer, DamageHandler dmg)
    {
        if (Random.Range(1,100) <= chance)
        {
            target.RefCharacter.Effectable.AddStatus(new Stun(stunDutration), dealer);
        }
    }
    
}
