using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SeaHorsePassive", menuName = "Passives/SeaHorse")]
public class SeaHorsePassive : AnimalPassive
{
    //every 10th attack that hits applys a damage debuff
    [SerializeField, Range(0, 10)] private float duration;
    [SerializeField, Range(0, 1)] private float amount;
    public override void SubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnHit.AddListener(ApplyDamageDebuff);
    }

    public override void UnSubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnHit.RemoveListener(ApplyDamageDebuff);
    }

    private void ApplyDamageDebuff(Damageable target, AnimalAttack attack, DamageDealer dealer, DamageHandler dmg)
    {
        if (dealer.RefCharacter.Counter.CurrentCounter % 10 == 0)
        {
            target.RefCharacter.Effectable.AddStatus(new DamageDebuff(duration, amount), dealer);
        }
    }
}
