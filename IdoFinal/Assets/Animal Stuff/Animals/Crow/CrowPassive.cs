using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CrowPassive", menuName = "Passives/Crow")]
public class CrowPassive : AnimalPassive
{
    //gain a damage buff after taking damage from birds
    [SerializeField, Range(5,10)] private float duration; 
    [SerializeField, Range(0,1)] private float amount; 
    public override void SubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.Damageable.OnGetHit.AddListener(DamageBuffAfterTakingDamageFromBirds);
    }

    public override void UnSubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.Damageable.OnGetHit.RemoveListener(DamageBuffAfterTakingDamageFromBirds);
    }

    private void DamageBuffAfterTakingDamageFromBirds(AnimalAttack attack, Damageable target, DamageDealer dealer, DamageHandler dmg)
    {
        if (dealer.RefAnimal.AnimalClass == AnimalClass.Bird)
        {
            target.RefCharacter.Effectable.AddStatus(new DamageBuff(duration, amount), target.RefCharacter.DamageDealer);
        }
    }
}
