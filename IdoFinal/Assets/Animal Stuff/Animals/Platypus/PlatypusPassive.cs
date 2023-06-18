using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlatypusPassive", menuName = "Passives/Platypus")]
public class PlatypusPassive : AnimalPassive
{
    //poison aquatic creatures on a hit
    //poison land creatures when hit
    [SerializeField, Range(0, 100)] private float totalAmount;
    [SerializeField, Range(0, 10)] private float duration;
    public override void SubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnHit.AddListener(PoisonEnemyHit);
        givenCaharacter.Damageable.OnGetHit.AddListener(PoisonEnemyWhenHit);
    }

    public override void UnSubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnHit.RemoveListener(PoisonEnemyHit);
        givenCaharacter.Damageable.OnGetHit.RemoveListener(PoisonEnemyWhenHit);
    }

    private void PoisonEnemyHit(Damageable target, AnimalAttack attack, DamageDealer dealer, DamageHandler dmg)
    {
        if (target.RefAnimal.Habitat == Habitat.FreshWater || target.RefAnimal.Habitat == Habitat.SaltWater)
        {
            target.RefCharacter.Effectable.AddStatus(new Poison(duration, totalAmount), dealer);
        }
    }
    private void PoisonEnemyWhenHit(AnimalAttack attack, Damageable target, DamageDealer dealer, DamageHandler dmg)
    {
        if (target.RefAnimal.Habitat != Habitat.FreshWater && target.RefAnimal.Habitat != Habitat.SaltWater)
        {
            dealer.RefCharacter.Effectable.AddStatus(new Poison(duration, totalAmount), target.RefCharacter.DamageDealer);
        }
    }
}
