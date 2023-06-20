using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CrabPassive", menuName = "Passives/Crab")]
public class CrabPassive : AnimalPassive
{
    //gain a defense buff after taking damage when swimming
    [SerializeField, Range(0, 10)] private float duration;
    [SerializeField, Range(0, 1)] private float amount;
    public override void SubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.Damageable.OnGetHit.AddListener(ApplyDefenseBuff);
    }

    public override void UnSubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.Damageable.OnGetHit.RemoveListener(ApplyDefenseBuff);
    }

    private void ApplyDefenseBuff(AnimalAttack attack, Damageable target, DamageDealer dealer, DamageHandler dmg)
    {
        if (target.RefCharacter.MovementMode == MovementMode.Water)
        {
            target.RefCharacter.Effectable.AddStatus(new DefenseBuff(duration, amount), target.RefCharacter.DamageDealer);
        }
    }
}
