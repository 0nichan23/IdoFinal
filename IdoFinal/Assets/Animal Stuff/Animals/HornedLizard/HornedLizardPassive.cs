using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HornedLizardPassive", menuName = "Passives/HornedLizard")]
public class HornedLizardPassive : AnimalPassive
{
    //deal true damage back when attacked (thorns only i didnt want to implement a status effect)
    [SerializeField, Range(0,10)] private float returnDamageAmount;
    public override void SubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.Damageable.OnGetHit.AddListener(ReturnDamage);
    }

    public override void UnSubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.Damageable.OnGetHit.RemoveListener(ReturnDamage);
    }

    private void ReturnDamage(AnimalAttack attack, Damageable target, DamageDealer dealer, DamageHandler dmg)
    {
        dealer.RefCharacter.Damageable.TakeTrueDamage(returnDamageAmount);
    }
}
