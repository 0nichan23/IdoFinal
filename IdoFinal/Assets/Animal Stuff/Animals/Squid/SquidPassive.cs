using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SquidPassive", menuName = "Passives/Squid")]
public class SquidPassive : AnimalPassive
{
    //apply accuracy debuff after taking damage in the water 
    [SerializeField, Range(0, 10)] private float duration;
    [SerializeField, Range(0, 1)] private float amount;
    public override void SubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.Damageable.OnGetHit.AddListener(ApplyAccuracyDebuff);
    }

    public override void UnSubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.Damageable.OnGetHit.RemoveListener(ApplyAccuracyDebuff);
    }

    private void ApplyAccuracyDebuff(AnimalAttack attack, Damageable target, DamageDealer dealer, DamageHandler dmg)
    {
        if (target.RefCharacter.MovementMode == MovementMode.Water)
        {
            dealer.RefCharacter.Effectable.AddStatus(new HitChanceDebuff(duration, amount), dealer);
        }
    }
}
