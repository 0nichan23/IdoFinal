using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PeacockPassive", menuName = "Passives/Peacock")]
public class PeacockPassive : AnimalPassive
{
    //take more damage in the air but less on the ground or water
    [SerializeField, Range(0, 1)] private float damageReduction;
    [SerializeField, Range(0, 1)] private float damageIncrease;
    public override void SubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.Damageable.OnGetHit.AddListener(AddDamageMod);
    }

    public override void UnSubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.Damageable.OnGetHit.RemoveListener(AddDamageMod);
    }

    private void AddDamageMod(AnimalAttack attack, Damageable target, DamageDealer dealer, DamageHandler dmg)
    {
        if (target.RefCharacter.MovementMode == MovementMode.Air)
        {
            dmg.AddMod(1 + damageIncrease);
        }
        else
        {
           dmg.AddMod(1 - damageReduction);
        }
    }
}
