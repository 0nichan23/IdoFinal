using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ReinDeerPassive", menuName = "Passives/ReinDeer")]

public class ReinDeerPassive : AnimalPassive
{
    //does more damage to carnivores but less damage to herbivores (good kids and bad kids)
    [SerializeField, Range(0,1)] private float carnivoresMod;
    [SerializeField, Range(0,0.9f)] private float herbivoresMod;
    public override void SubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnHit.AddListener(DamageMod);
    }

    public override void UnSubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnHit.RemoveListener(DamageMod);
    }

    private void DamageMod(Damageable target, AnimalAttack attack, DamageDealer dealer, DamageHandler dmg)
    {
        if (target.RefAnimal.Diet == Diet.Carnivore)
        {
            dmg.AddMod(1 + carnivoresMod);
        }
        else if (target.RefAnimal.Diet == Diet.Herbivore)
        {
            dmg.AddMod(1 - herbivoresMod);
        }
    }
}
