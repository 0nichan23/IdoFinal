using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SeaLionPassive", menuName = "Passives/SeaLion")]

public class SeaLionPassive : AnimalPassive
{

    [SerializeField, Range(5,10)] private float duration;
    [SerializeField, Range(0,1)] private float damageMod;

    //gain a damage buff whenever inflicting bleed
    public override void SubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnApplyStatus.AddListener(GainDamageBuffOnInflictingBleed);
    }

    public override void UnSubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnApplyStatus.RemoveListener(GainDamageBuffOnInflictingBleed);
    }

    private void GainDamageBuffOnInflictingBleed(StatusEffect status, Effectable target, DamageDealer dealer)
    {
        if (status is Bleed)
        {
            dealer.RefCharacter.Effectable.AddStatus(new DamageBuff(duration, damageMod), dealer);
        }
    }
}
