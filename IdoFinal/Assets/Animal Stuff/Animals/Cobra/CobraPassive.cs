using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CobraPassive", menuName = "Passives/Cobra")]
public class CobraPassive : AnimalPassive
{
    //hits have a chance to apply an accuracy debuff
    [SerializeField, Range(0,10)] private float duration;
    [SerializeField, Range(0,1)] private float amount;
    [SerializeField, Range(0,100)] private float chance;
    public override void SubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnHit.AddListener(HitChanceDebuffOnHit);
    }

    public override void UnSubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnHit.RemoveListener(HitChanceDebuffOnHit);
    }


    private void HitChanceDebuffOnHit(Damageable target, AnimalAttack attack, DamageDealer dealer, DamageHandler dmg)
    {
        if (Random.Range(0,100) <= chance)
        {
            target.RefCharacter.Effectable.AddStatus(new HitChanceDebuff(duration, amount), dealer);
        }
    }
}
