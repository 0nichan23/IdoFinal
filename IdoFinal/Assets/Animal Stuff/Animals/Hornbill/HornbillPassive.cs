using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "HornbillPassive", menuName = "Passives/Hornbill")]
public class HornbillPassive : AnimalPassive
{
    //resotre health after killing herbivores 
    [SerializeField, Range(0,1)] private float amount;
    public override void SubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnKill.AddListener(HealOnHerbivoreKill);
    }

    public override void UnSubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnKill.RemoveListener(HealOnHerbivoreKill);
    }

    private void HealOnHerbivoreKill(Damageable target, DamageDealer dealer)
    {
        if (target.RefAnimal.Diet == Diet.Herbivore)
        {
            dealer.RefCharacter.Damageable.Heal(new DamageHandler() { BaseAmount = dealer.RefCharacter.Damageable.MaxHp * amount});
        }
    }
}
