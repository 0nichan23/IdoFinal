using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PelicanPassive", menuName = "Passives/Pelican")]
public class PelicanPassive : AnimalPassive
{
    //deal more damage to fish
    [SerializeField, Range(0, 1)] private float dmgMod;
    public override void SubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnHit.AddListener(IncreaseDamage);
    }

    public override void UnSubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnHit.RemoveListener(IncreaseDamage);
    }

    private void IncreaseDamage(Damageable target, AnimalAttack attack, DamageDealer dealer, DamageHandler dmg)
    {
        if (target.RefAnimal.AnimalClass == AnimalClass.Fish)
        {
            dmg.AddMod(1 + dmgMod);
        }
    }
}
