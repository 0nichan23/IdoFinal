using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HippoPassive", menuName = "Passives/Hippo")]

public class HippoPassive : AnimalPassive
{
    //take 30% less damage from aquatic creatures.
    [SerializeField, Range(0,1)] private float damageReduction;
    public override void SubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.Damageable.OnGetHit.AddListener(ReduceDamageFromAquaticCreatures);
    }

    public override void UnSubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.Damageable.OnGetHit.RemoveListener(ReduceDamageFromAquaticCreatures);
    }


    private void ReduceDamageFromAquaticCreatures(AnimalAttack givenAttack, Damageable target, DamageDealer dealer)
    {
        if (dealer.RefAnimal.Habitat == Habitat.FreshWater || dealer.RefAnimal.Habitat == Habitat.SaltWater)
        {
            givenAttack.Damage.AddMod(1 - damageReduction);
        }
    }

}
