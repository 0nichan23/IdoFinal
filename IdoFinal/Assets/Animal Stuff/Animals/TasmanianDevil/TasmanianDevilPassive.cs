using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TasmanianDevilPassive", menuName = "Passives/TasmanianDevil")]
public class TasmanianDevilPassive : AnimalPassive
{
    //gain an armor pen buff when attacking a bleeding target
    [SerializeField, Range(0, 10)] private float duration;
    [SerializeField, Range(0, 10)] private int amount;
    public override void SubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnHit.AddListener(GainArmorPenBuff);
    }

    public override void UnSubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnHit.RemoveListener(GainArmorPenBuff);
    }

    private void GainArmorPenBuff(Damageable target, AnimalAttack attack, DamageDealer dealer, DamageHandler dmg)
    {
        foreach (var item in target.RefCharacter.Effectable.ActiveEffects)
        {
            if (item is Bleed)
            {
                dealer.RefCharacter.Effectable.AddStatus(new ArmorPenBuff(duration, amount), dealer);
            }
        }
    }
}
