using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GazellePassive", menuName = "Passives/Gazelle")]
public class GazellePassive : AnimalPassive
{
    //gain a dodge chance buff after being struck by a carnivore
    [SerializeField, Range(5, 10)] private float duration;
    [SerializeField, Range(0, 1)] private float amount;
    public override void SubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.Damageable.OnGetHit.AddListener(GainDodgeChance);
    }

    public override void UnSubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.Damageable.OnGetHit.RemoveListener(GainDodgeChance);
    }

    private void GainDodgeChance(AnimalAttack attack, Damageable target, DamageDealer dealer, DamageHandler dmg)
    {
        if (dealer.RefAnimal.Diet == Diet.Carnivore)
        {
            target.RefCharacter.Effectable.AddStatus(new DodgeChanceBuff(duration, amount), target.RefCharacter.DamageDealer);
        }
    }
}
