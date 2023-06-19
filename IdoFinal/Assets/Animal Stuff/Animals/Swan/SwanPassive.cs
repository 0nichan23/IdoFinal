using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SwanPassive", menuName = "Passives/Swan")]
public class SwanPassive : AnimalPassive
{
    //gain a damage buff after taking damage in the water
    [SerializeField, Range(0, 10)] private float duration; 
    [SerializeField, Range(0, 1)] private float amount; 
    public override void SubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.Damageable.OnGetHit.AddListener(AddDamageBuffInTheWater);
    }

    public override void UnSubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.Damageable.OnGetHit.RemoveListener(AddDamageBuffInTheWater);
    }

    private void AddDamageBuffInTheWater(AnimalAttack attack, Damageable target, DamageDealer dealer, DamageHandler dmg)
    {
        if (target.RefCharacter.MovementMode == MovementMode.Water)
        {
            target.RefCharacter.Effectable.AddStatus(new DamageBuff(duration, amount), target.RefCharacter.DamageDealer);
        }
    }

  
}
