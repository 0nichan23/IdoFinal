using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Animal", menuName = "Passives/Test")]
public class TestPassive : AnimalPassive
{
    public override void SubscribePassive(Character givenCharacter)
    {
        givenCharacter.DamageDealer.OnHit.AddListener(ExtraDamageOnDesertCreatures);
    }

    public override void UnSubscribePassive(Character givenCharacter)
    {
        givenCharacter.DamageDealer.OnHit.RemoveListener(ExtraDamageOnDesertCreatures);
    }

    private void ExtraDamageOnDesertCreatures(Damageable target, AnimalAttack attack)
    {
        if (target.RefAnimal.Habitat == Habitat.Desert || target.RefAnimal.Habitat == Habitat.FreshWater)
        {
            attack.Damage.AddMod(1.2f);
        }
    }

}
