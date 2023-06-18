using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GoldenEaglePassive", menuName = "Passives/GoldenEagle")]
public class GoldenEaglePassive : AnimalPassive
{
    //increased damage against samll creatures 
    [SerializeField, Range(0, 1)] private float damageIncrease;
    public override void SubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnHit.AddListener(DamageIncrease);
    }

    public override void UnSubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnHit.RemoveListener(DamageIncrease);
    }

    private void DamageIncrease(Damageable target, AnimalAttack attack, DamageDealer delaer, DamageHandler dmg)
    {
        if (target.RefAnimal.Size == Size.Small)
        {
            dmg.AddMod(damageIncrease);
        }
    }
}
