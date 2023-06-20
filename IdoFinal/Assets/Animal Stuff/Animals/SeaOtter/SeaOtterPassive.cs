using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SeaOtterPassive", menuName = "Passives/SeaOtter")]
public class SeaOtterPassive : AnimalPassive
{
    //gain an attack speed and crit hit buffs when inflicting bleed
    [SerializeField, Range(0, 10)] private float aspeedDuration;
    [SerializeField, Range(0, 1)] private float aspeedMod;
    [SerializeField, Range(0, 10)] private float critHitDuration;
    [SerializeField, Range(0, 1)] private float critHitMod;
    public override void SubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnApplyStatus.AddListener(ApplyStatusEffects);
    }

    public override void UnSubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnApplyStatus.RemoveListener(ApplyStatusEffects);
    }

    private void ApplyStatusEffects(StatusEffect effect, Effectable target, DamageDealer dealer)
    {
        if (effect is Bleed)
        {
            dealer.RefCharacter.Effectable.AddStatus(new AttackSpeedBuff(aspeedDuration, aspeedMod), dealer);
            dealer.RefCharacter.Effectable.AddStatus(new CritHitBuff(critHitDuration, critHitMod), dealer);
        }
    }
}
